using Infrastructure.RaycastService;
using NSubstitute;
using NUnit.Framework;
using Zenject;

namespace Domain.ARAlignmentService.Tests
{
    public class ARObjectAlignmentTest
    {
        private DiContainer container;
        
        private ARObjectAlignment aRObjectAlignment;
        private IRaycastSystem raycastSystem;
        
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


