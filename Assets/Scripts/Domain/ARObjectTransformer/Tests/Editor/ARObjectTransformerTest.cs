using NSubstitute;
using NUnit.Framework;
using Zenject;

namespace Domain.ARObjectService.Tests
{
    public class ARObjectTransformerTest
    {
        private DiContainer container;
        
        private ARObjectTransformer aRObjectTransformer;
        
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
            container.Inject(this);
        }
        
        [Test]
        public void Test()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
    }
}


