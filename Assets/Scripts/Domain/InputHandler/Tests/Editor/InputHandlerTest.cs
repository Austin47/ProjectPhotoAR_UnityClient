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

        private void Construct(InputHandler inputHandler)
        {
            this.inputHandler = inputHandler;
        }

        [SetUp]
        public void SetUp()
        {
            inputSystem = Substitute.For<IInputSystem>();
            container.Bind<InputHandler>().AsSingle();
            container.Inject(this);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void FireEventOnTouch(bool isTouching, int[] pos)
        {
            // Arrange
            bool touched = false;
            inputSystem.IsTouching.Returns(isTouching);
            Action<Vector2> action = (v) => touched = true;
            //inputHandler.OnTouch1 += Raise.Event<Action<Vector2>>(action);

            // Act
            inputHandler.Tick();

            // Assert
            Assert.Equals(isTouching, touched);
        }

        [TestCase(new int[] { 5, 5 })]
        [TestCase(new int[] { 0, -1 })]
        [TestCase(new int[] { -231231, 2312312 })]
        public void GetTouch1Position(int[] pos)
        {
            // Arrange
            // set player is pressing button
            bool success = false;
            Vector2 posV = new Vector2(pos[0], pos[1]);
            inputSystem.Touch1Pos.Returns(posV);
            Action<Vector2> action = (v) => success = v == posV;
            //inputHandler.OnTouch1 += Raise.Event<Action<Vector2>>(action);

            // Act
            inputHandler.Tick();

            // Assert
            Assert.True(success);
        }
    }
}

