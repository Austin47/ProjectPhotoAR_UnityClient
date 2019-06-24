using System;
using Infrastructure.CCSystem;
using Infrastructure.DatabaseService;
using PhotoGalleryService;
using UnityEngine;
using Zenject;

namespace Domain.CameraCaptureService
{
    public class CameraCapture
    {
        public event Action<Texture2D> OnCapturePhoto;

        private ICameraCaptureSystem cameraCaptureSystem;
        private IDatabase database;
        private IPhotoGallery photoGallery;

        [Inject]
        public void Construct(
            ICameraCaptureSystem cameraCaptureSystem,
            IDatabase database,
            IPhotoGallery photoGallery)
        {
            this.cameraCaptureSystem = cameraCaptureSystem;
            this.database = database;
            this.photoGallery = photoGallery;
        }

        public void CapturePhoto() => cameraCaptureSystem.CapturePhoto(SavePhoto);
        private void SavePhoto(byte[] photo)
        {
            //database.SavePhoto(photo, OnSavedPhoto);
            // Temp logic
            Texture2D text = new Texture2D(1, 1);
            //text.LoadImage(photo);
            OnGetSavedPhoto(text);
        }
        private void OnSavedPhoto(string path) => /*photoGallery.LoadPhoto(path, OnGetSavedPhoto*/ Debug.Log("TODO: OnSavedPhoto");
        private void OnGetSavedPhoto(Texture2D texture)
        {
            var handler = OnCapturePhoto;
            if (handler != null) handler(texture);
        }
    }
}


