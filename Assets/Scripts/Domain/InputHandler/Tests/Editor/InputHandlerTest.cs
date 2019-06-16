using System;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Domain.InputService.Tests
{
    public class InputHandlerTest
    {
        private DiContainer container;
        public InputHandler inputHandler;
        public IInputSystem inputSystem;

        [Inject]
        private void Construct(InputHandler inputHandler)
        {
            this.inputHandler = inputHandler;
        }

        [SetUp]
        public void SetUp()
        {
            container = new DiContainer();
            inputSystem = Substitute.For<IInputSystem>();
            container.Bind<IInputSystem>().FromInstance(inputSystem);
            container.Bind<InputHandler>().AsSingle();
            container.Inject(this);
        }

        [TestCase(true)]
        public void FireEventOnTap_True(bool wasTapped)
        {
            // Arrange
            bool touch = false;
            Action<Vector2> action = (v) => touch = true;
            inputHandler.OnTap += action;

            // Act
            inputSystem.OnTap += Raise.Event<Action<Vector2>>(new Vector2());

            // Assert
            Assert.AreEqual(wasTapped, touch);
        }

        [TestCase(false)]
        public void FireEventOnTap_False(bool wasTapped)
        {
            // Arrange
            bool touch = false;
            Action<Vector2> action = (v) => touch = true;
            inputHandler.OnTap += action;
            inputHandler.Dispose();

            // Act
            inputSystem.OnTap += Raise.Event<Action<Vector2>>(new Vector2());

            // Assert
            Assert.AreEqual(wasTapped, touch);
        }

        [TestCase(new int[] { 0, 0 })]
        [TestCase(new int[] { -70238, -67438 })]
        [TestCase(new int[] { 38590, 231223 })]
        [TestCase(new int[] { 23131, -113 })]
        public void FireEventOnTap_CorrectPos(int[] pos)
        {
            // Arrange
            Vector2 actual = new Vector2();
            Vector2 expected = new Vector2(pos[0], pos[1]);
            Action<Vector2> action = (v) => actual = v;
            inputHandler.OnTap += action;

            // Act
            inputSystem.OnTap += Raise.Event<Action<Vector2>>(expected);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

