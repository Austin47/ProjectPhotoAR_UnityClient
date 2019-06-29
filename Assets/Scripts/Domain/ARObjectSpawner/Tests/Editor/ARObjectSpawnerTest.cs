using Domain.ARAlignmentService;
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

        public IARObject aRObject;
        private IARObjectAlignment aRObjectAlignment;
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

            aRObject = Substitute.For<IARObject>();
            container.BindMemoryPool<IARObject, ARObjectPool>().FromInstance(aRObject);

            aRObjectAlignment = Substitute.For<IARObjectAlignment>();
            container.Bind<IARObjectAlignment>().FromInstance(aRObjectAlignment);

            container.Bind(typeof(ARObjectSpawner)).To<ARObjectSpawner>().AsSingle();
            container.Inject(this);
        }


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

        [Test]
        public void SpawnAndConfigure()
        {
            // Arrange
            var objectConfigured = false;

            // Act
            aRObjectSpawner.Spawn(Arg.Any<Texture2D>());

            // Assert
            aRObject.Received().Configure(Arg.Any<Vector3>(), Arg.Any<Texture2D>());
        }
    }
}


