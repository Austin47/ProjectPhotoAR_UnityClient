using NSubstitute;
using NUnit.Framework;
using Zenject;

namespace Domain.ARObjectSpawner.Tests
{
    public class ARObjectSpawner
    {
        private DiContainer container;
        
        private ARObjectSpawner aRObjectSpawner;
        
        [Inject]
        private void Construct(ARObjectSpawner aRObjectSpawner)
        {
            this.aRObjectSpawner = aRObjectSpawner;
        }
        
        [SetUp]
        public void SetUp()
        {
            container = new DiContainer();
            container.Bind<ARObjectSpawner>().AsSingle();
            container.Inject(this);
        }
        
        [Test]
        public void Test()
        {
            // Arrange
            // based on the input spawn an object
            // spawns a AIObject
            
            // Act
            
            // Assert
        }
    }
}


