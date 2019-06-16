using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Domain.InputService
{
    public class InputHandler : IInputHandler, ITickable
    {
        private IInputSystem inputSystem;

        public event Action<Vector2> OnTouchPos1;
        public event Action<Vector2> OnTouch;

        [Inject]
        private void Construct(IInputSystem inputSystem)
        {
            this.inputSystem = inputSystem;
        }

        public void Tick()
        {
            if (!inputSystem.IsTouching) return;
        }
    }
}

