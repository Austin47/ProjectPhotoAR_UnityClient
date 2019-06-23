using System;
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

        [SetUp]
        public void SetUp()
        {
            container = new DiContainer();

            raycastSystem = Substitute.For<IRaycastSystem>();
            container.Bind<IRaycastSystem>().FromInstance(raycastSystem).AsSingle();

            var aRObject = Substitute.For<IARObject>();
            container.BindMemoryPool<IARObject, ARObjectPool>().FromInstance(aRObject);

            container.Bind(typeof(ARObjectSpawner)).To<ARObjectSpawner>().AsSingle();
            container.Inject(this);
        }


        // TODO: AT - Need more test, to insure objects are being spawned at the right position
        [Test]
        public void Spawn()
        {
            // Arrange
            int expectedCount = 1;

            // Act
            aRObjectSpawner.Spawn(Arg.Any<Texture2D>());

            // Assert
            Assert.AreEqual(expectedCount, aRObjectPool.NumTotal);
        }
    }
}


