using System;
using Infrastructure.CCSystem;
using Infrastructure.DatabaseService;
using NSubstitute;
using NUnit.Framework;
using PhotoGalleryService;
using UnityEngine;
using Zenject;

namespace Domain.CameraCaptureService.Tests
{
    public class CameraCaptureTest
    {
        // Take picture - with I ICameraCapture
        // save to the IDatabase
        // notify when complete
        // cant capture first image is done pressesing - queue pictures to be taken 
        // - NVM no need for queue CameraImage.ConvertAsync dose this automatically 
        private DiContainer container;

        private CameraCapture cameraCapture;
        private ICameraCaptureSystem cameraCaptureSystem;
        private IDatabase database;
        private IPhotoGallery photoGallery;

        [Inject]
        private void Construct(CameraCapture cameraCapture)
        {
            this.cameraCapture = cameraCapture;
        }

        [SetUp]
        public void SetUp()
        {
            container = new DiContainer();
            container.Bind<CameraCapture>().AsSingle();

            cameraCaptureSystem = Substitute.For<ICameraCaptureSystem>();
            container.Bind<ICameraCaptureSystem>().FromInstance(cameraCaptureSystem);

            database = Substitute.For<IDatabase>();
            container.Bind<IDatabase>().FromInstance(database);

            photoGallery = Substitute.For<IPhotoGallery>();
            container.Bind<IPhotoGallery>().FromInstance(photoGallery);

            container.Inject(this);
        }

        [Test]
        public void CapturePhotoCallCount()
        {
            // Arrange
            var callCount = 0;
            var expected = 4;
            var photoArray = new byte[] { 0, 1 };
            var photoPath = "path";
            var photoTexture = new Texture2D(1, 1);
            cameraCaptureSystem.CapturePhoto(Arg.Do<Action<byte[]>>(cb =>
            {
                callCount++;
                cb(photoArray);
            }));
            database.SavePhoto(photoArray, Arg.Do<Action<string>>(cb =>
            {
                callCount++;
                cb(photoPath);
            }));
            photoGallery.LoadPhoto(photoPath, Arg.Do<Action<Texture2D>>(cb =>
            {
                callCount++;
                cb(photoTexture);
            }));
            cameraCapture.OnCapturePhoto += t =>
            {
                if (t == photoTexture)
                {
                    callCount++;
                }
            };

            // Act
            cameraCapture.CapturePhoto();

            // Assert
            Assert.AreEqual(expected, callCount);
        }

        [Test]
        public void CapturePhotoEndtTexture()
        {
            // Arrange
            var success = false;
            var photoArray = new byte[] { 0, 1 };
            var photoPath = "path";
            var photoTexture = new Texture2D(1, 1);
            cameraCaptureSystem.CapturePhoto(Arg.Do<Action<byte[]>>(cb => cb(photoArray)));
            database.SavePhoto(photoArray, Arg.Do<Action<string>>(cb => cb(photoPath)));
            photoGallery.LoadPhoto(photoPath, Arg.Do<Action<Texture2D>>(cb => cb(photoTexture)));
            cameraCapture.OnCapturePhoto += t =>
            {
                if (t == photoTexture)
                {
                    success = true;
                }
            };

            // Act
            cameraCapture.CapturePhoto();

            // Assert
            Assert.IsTrue(success);
        }
    }
}


