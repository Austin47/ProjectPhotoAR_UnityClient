using Domain.ARObjectSpawnService;
using Infrastructure.CameraService;
using Infrastructure.Common;
using Infrastructure.RaycastService;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Domain.ARAlignmentService.Tests
{
    public class ARObjectAlignmentTest
    {
        private DiContainer container;
        
        private ARObjectAlignment aRObjectAlignment;
        private ICameraSystem cameraSystem;
        private IRaycastSystem raycastSystem;
        private IARObject aRObject;
        
        [Inject]
        private void Construct(ARObjectAlignment aRObjectAlignment)
        {
            this.aRObjectAlignment = aRObjectAlignment;
        }
        
        [SetUp]
        public void SetUp()
        {
            container = new DiContainer();
            container.Bind<ARObjectAlignment>().AsSingle();

            raycastSystem = Substitute.For<IRaycastSystem>();
            container.Bind<IRaycastSystem>().FromInstance(raycastSystem);

            cameraSystem = Substitute.For<ICameraSystem>();
            container.Bind<ICameraSystem>().FromInstance(cameraSystem);

            aRObject = Substitute.For<IARObject>();

            container.Inject(this);
        }
        
        [Test]
        public void TestPlaceObject()
        {
            // Arrange
            aRObjectAlignment.RegistererUnaligned(aRObject);
            var blankVector = Vector3.zero;
            var cameraPos = new Vector3(1, 4, 6);
            var aRObjectPos = new Vector3(0, 1, 2);
            var planePoint = new Vector3(0, 5, 0);
            cameraSystem.pos.Returns(cameraPos);
            aRObject.pos.Returns(aRObjectPos);
            aRObject.IsVisible.Returns(true);
            raycastSystem.TryToGetPlanePoint(
                cameraPos, 
                Utils.GetDirectionBetweenVectors(cameraPos, aRObjectPos),
                 out blankVector).Returns( x => {
                     x[2] = planePoint;
                     return true;
                 });

            // Act
            aRObjectAlignment.Tick();
            
            // Assert
            aRObject.Received().SetPosition(planePoint);
        }
    }
}


