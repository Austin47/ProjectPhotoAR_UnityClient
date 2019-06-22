using System;
using Infrastructure.InputService;
using Infrastructure.RaycastService;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Domain.ARObjectSpawnService.Tests
{
    public class ARObjectSpawnerTest
    {
        private DiContainer container;

        private ARObjectSpawner aRObjectSpawner;
        private IInputHandler inputHandler;
        private ARObjectPool aRObjectPool;
        private IRaycastSystem raycastSystem;

        [Inject]
        private void Construct(
            ARObjectSpawner aRObjectSpawner,
            ARObjectPool aRObjectPool)
        {
            this.aRObjectSpawner = aRObjectSpawner;
            this.aRObjectPool = aRObjectPool;
        }

        // [SetUp]
        // public void SetUp()
        // {
        //     container = new DiContainer();

        //     raycastSystem = Substitute.For<IRaycastSystem>();
        //     container.Bind<IRaycastSystem>().FromInstance(raycastSystem).AsSingle();

        //     inputHandler = Substitute.For<IInputHandler>();
        //     container.Bind<IInputHandler>().FromInstance(inputHandler).AsSingle();

        //     var aRObject = Substitute.For<IARObject>();
        //     container.BindMemoryPool<IARObject, ARObjectPool>().FromInstance(aRObject);

        //     container.Bind(typeof(ARObjectSpawner), typeof(IInitializable)).To<ARObjectSpawner>().AsSingle();
        //     container.Inject(this);
        //     aRObjectSpawner.Initialize();
        // }

        // [TestCase(true)]
        // [TestCase(false)]
        // public void TrySpawnObject(bool expected)
        // {
        //     // Arrange
        //     Vector3 blankVector = new Vector3();
        //     raycastSystem.TryTouchPosToARPlane(Arg.Any<Vector2>(), out blankVector).Returns(expected);

        //     // Act
        //     inputHandler.OnTap += Raise.Event<Action<Vector2>>(new Vector2());

        //     // Assert
        //     var expectedCount = expected ? 1 : 0;
        //     Assert.AreEqual(expectedCount, aRObjectPool.NumTotal);
        // }

        // [Test]
        // public void TrySpawnObject_Fail()
        // {
        //     // Arrange
        //     Vector3 blankVector = new Vector3();
        //     raycastSystem.TryTouchPosToARPlane(Arg.Any<Vector2>(), out blankVector).Returns(true);
        //     aRObjectSpawner.Dispose();
        //     // Act
        //     inputHandler.OnTap += Raise.Event<Action<Vector2>>(new Vector2());

        //     // Assert
        //     Assert.AreEqual(0, aRObjectPool.NumTotal);
        // }
    }
}


