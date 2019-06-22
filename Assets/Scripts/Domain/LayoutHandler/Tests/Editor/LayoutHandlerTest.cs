using NSubstitute;
using NUnit.Framework;
using Zenject;
using UnityEngine.TestTools;

namespace Domain.LayoutHandlerService.Tests
{
    public class LayoutHandlerTest
    {
        private DiContainer container;

        private LayoutHandler layoutHandler;

        [Inject]
        private void Construct(LayoutHandler layoutHandler)
        {
            this.layoutHandler = layoutHandler;
        }

        [SetUp]
        public void SetUp()
        {
            container = new DiContainer();
            container.Bind<LayoutHandler>().AsSingle();
            container.Inject(this);
        }

        [Test]
        public void AddDefaultLayout_Pass_1()
        {
            // Arrange
            ILayoutEntity defaultLayout = Substitute.For<ILayoutEntity>();

            // Act
            layoutHandler.SetDefaultLayout(defaultLayout);

            // Assert
            Assert.AreEqual(defaultLayout, layoutHandler.DefaultLayout);
        }

        [Test]
        public void AddDefaultLayout_Pass_2()
        {
            // Arrange
            ILayoutEntity defaultLayout = Substitute.For<ILayoutEntity>();
            layoutHandler.SetDefaultLayout(defaultLayout);

            // Act
            layoutHandler.SetDefaultLayout(Substitute.For<ILayoutEntity>());

            // Assert
            // TODO: AT - this detects an error that should be logged in SetDefaultLayout
            // But it also logs it, this lets us know the test is passing, but its very confusing
            LogAssert.Expect(UnityEngine.LogType.Error, "LayoutHandler: SetDefaultLayout: defaultLayout has already been set!");
        }

        [Test]
        public void AddDefaultLayout_Pass_3()
        {
            // Arrange
            ILayoutEntity defaultLayout = Substitute.For<ILayoutEntity>();

            // Act
            layoutHandler.SetDefaultLayout(defaultLayout);

            // Assert
            Assert.AreEqual(defaultLayout, layoutHandler.TopLayout);
        }

        [Test]
        public void AddLayout_Pass_1()
        {
            // Arrange
            ILayoutEntity defaultLayout = Substitute.For<ILayoutEntity>();
            layoutHandler.SetDefaultLayout(defaultLayout);
            ILayoutEntity layout1 = Substitute.For<ILayoutEntity>();

            // Act
            layoutHandler.ShowLayout(layout1);

            // Assert
            Assert.AreEqual(layout1, layoutHandler.TopLayout);
        }

        [Test]
        public void AddLayout_Pass_2()
        {
            // Arrange
            ILayoutEntity defaultLayout = Substitute.For<ILayoutEntity>();
            layoutHandler.SetDefaultLayout(defaultLayout);
            ILayoutEntity layout1 = Substitute.For<ILayoutEntity>();
            ILayoutEntity layout2 = Substitute.For<ILayoutEntity>();
            layout2.Order.Returns(1);
            ILayoutEntity layout3 = Substitute.For<ILayoutEntity>();
            layout3.Order.Returns(2);
            ILayoutEntity layout4 = Substitute.For<ILayoutEntity>();
            layout4.Order.Returns(1);

            // Act
            layoutHandler.ShowLayout(layout1);
            layoutHandler.ShowLayout(layout2);
            layoutHandler.ShowLayout(layout3);
            layoutHandler.ShowLayout(layout4);

            // Assert
            Assert.AreEqual(layout4, layoutHandler.TopLayout);
        }

        [Test]
        public void GoBack_Pass_1()
        {
            // Arrange
            ILayoutEntity defaultLayout = Substitute.For<ILayoutEntity>();
            layoutHandler.SetDefaultLayout(defaultLayout);
            ILayoutEntity layout1 = Substitute.For<ILayoutEntity>();
            layout1.Order.Returns(1);
            ILayoutEntity layout2 = Substitute.For<ILayoutEntity>();
            layout2.Order.Returns(2);
            ILayoutEntity layout3 = Substitute.For<ILayoutEntity>();
            layout3.Order.Returns(3);
            ILayoutEntity layout4 = Substitute.For<ILayoutEntity>();
            layout4.Order.Returns(2);

            // Act
            layoutHandler.ShowLayout(layout1);
            layoutHandler.ShowLayout(layout2);
            layoutHandler.ShowLayout(layout3);
            layoutHandler.ShowLayout(layout4);
            layoutHandler.GoBack();

            // Assert
            Assert.AreEqual(layout1, layoutHandler.TopLayout);
        }
    }
}


