using System;
using Domain.ARObjectSpawnService;
using Infrastructure.CameraService;
using Infrastructure.InputService;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Domain.ARObjectService.Tests
{
    public class ARObjectTransformerTest
    {
        private DiContainer container;

        private ARObjectTransformer aRObjectTransformer;
        private ICameraSystem cameraSystem;
        private IInputSystem inputSystem;

        [Inject]
        private void Construct(ARObjectTransformer aRObjectTransformer)
        {
            this.aRObjectTransformer = aRObjectTransformer;
        }

        [SetUp]
        public void SetUp()
        {
            container = new DiContainer();
            container.Bind<ARObjectTransformer>().AsSingle();

            cameraSystem = Substitute.For<ICameraSystem>();
            container.Bind<ICameraSystem>().FromInstance(cameraSystem);

            inputSystem = Substitute.For<IInputSystem>();
            container.Bind<IInputSystem>().FromInstance(inputSystem);

            container.Inject(this);
        }

        [Test]
        public void SetARObject_Pass_1()
        {
            // Arrange
            var arObject = Substitute.For<IARObject>();

            // Act
            aRObjectTransformer.SetSelectedARObject(arObject);

            // Assert
            Assert.AreEqual(arObject, aRObjectTransformer.SelectedARObject);
        }

        [Test]
        public void SetARObject_Pass_2()
        {
            // Arrange
            var arObject1 = Substitute.For<IARObject>();
            var arObject2 = Substitute.For<IARObject>();

            // Act
            aRObjectTransformer.SetSelectedARObject(arObject1);
            aRObjectTransformer.SetSelectedARObject(arObject2);

            // Assert
            Assert.AreEqual(arObject1, aRObjectTransformer.SelectedARObject);
        }

        [Test]
        public void UpdateObjectPosition_Pass()
        {
            // Arrange
            // Declare variables
            var arObject = Substitute.For<IARObject>();
            var arObjectPos = new Vector3(0, 0, 1);
            var camPos = Vector3.zero;
            var touchPos = new Vector2(0, 5);
            var touchDis = new Vector3(touchPos.x, touchPos.y, 1);
            var worldPos = new Vector3(4, 5, 9);
            var expected = new Vector3(worldPos.x, arObjectPos.y, worldPos.z);
            Vector2 blankVector;
            // Set up function flow
            aRObjectTransformer.SetSelectedARObject(arObject);
            arObject.Pos.Returns(arObjectPos);
            cameraSystem.Pos.Returns(Vector3.zero);
            inputSystem.GetTouchPos(out blankVector).Returns(x =>
            {
                x[0] = touchPos;
                return true;
            });
            cameraSystem.ScreenToWorldPoint(touchDis).Returns(worldPos);

            aRObjectTransformer.Initialize();

            // Act
            inputSystem.OnPanHandler += Raise.Event<Action<Vector2>>(Vector2.zero);


            // Assert
            arObject.Received().SetPosition(expected);
        }

        [Test]
        public void UpdateObjectPosition_Fail()
        {
            // Arrange
            var arObject = Substitute.For<IARObject>();
            aRObjectTransformer.SetSelectedARObject(arObject);
            aRObjectTransformer.Initialize();

            // Act
            aRObjectTransformer.Dispose();
            inputSystem.OnPanHandler += Raise.Event<Action<Vector2>>(Vector2.zero);


            // Assert
            arObject.DidNotReceive().SetPosition(Arg.Any<Vector3>());
        }
    }
}


