using Domain.ARObjectSpawnService;
using Infrastructure.CameraService;
using Infrastructure.InputService;
using NSubstitute;
using NUnit.Framework;
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
        public void UpdateObjectPosition()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}


