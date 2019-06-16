using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.InputService
{
    public class TouchKitInputSystem : IInputSystem
    {
        public event Action<Vector2> OnTap;

        private TKTapRecognizer recognizer;

        public TouchKitInputSystem()
        {
            // TODO: Refactor - this creates a generic TapRecognizer that covers the screen
            // This will be a problem once ui is intergrated
            CreateTapRecognizer();
        }

        private void CreateTapRecognizer()
        {
            recognizer = new TKTapRecognizer();
            recognizer.gestureRecognizedEvent += OnTapCall;
            TouchKit.addGestureRecognizer(recognizer);
        }

        private void OnTapCall(TKTapRecognizer tap)
        {
            Vector2 pos = tap.touchLocation();
            var handler = OnTap;
            if (handler != null) handler(pos);
        }
    }
}

