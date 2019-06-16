using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Infrastructure.InputService
{
    public class InputHandler : IInputHandler, IDisposable
    {
        private IInputSystem inputSystem;

        public event Action<Vector2> OnTouchPos1;
        public event Action<Vector2> OnTap;

        [Inject]
        private void Construct(IInputSystem inputSystem)
        {
            this.inputSystem = inputSystem;
            Subscribe();
        }

        private void Subscribe()
        {
            inputSystem.OnTap += CallOnTap;
        }

        public void Dispose()
        {
            inputSystem.OnTap -= CallOnTap;
        }

        public void CallOnTap(Vector2 pos)
        {
            var handler = OnTap;
            if (handler != null) handler(pos);
        }
    }
}

