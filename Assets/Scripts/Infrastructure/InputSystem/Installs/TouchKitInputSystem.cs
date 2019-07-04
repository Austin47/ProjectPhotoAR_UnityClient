using System;
using UnityEngine;

namespace Infrastructure.InputService
{
    public class TouchKitInputSystem : IInputSystem, IDisposable
    {
        public bool IsTouching { get { return Input.touchCount > 0 || Input.GetMouseButton(0); } }
        public event Action<Vector2> OnPanHandler;
        public event Action<float> OnPinchHandler;
        public event Action<float> OnRotationHandler;

        private TKPanRecognizer panRecognizer;
        private TKPinchRecognizer pinchRecognizer;
        private TKRotationRecognizer rotationRecognizer;

        public TouchKitInputSystem()
        {
            panRecognizer = new TKPanRecognizer();
            pinchRecognizer = new TKPinchRecognizer();
            rotationRecognizer = new TKRotationRecognizer();

            panRecognizer.gestureRecognizedEvent += OnPan;
            pinchRecognizer.gestureRecognizedEvent += OnPinch;
            rotationRecognizer.gestureRecognizedEvent += OnRotation;

            TouchKit.addGestureRecognizer(panRecognizer);
            TouchKit.addGestureRecognizer(pinchRecognizer);
            TouchKit.addGestureRecognizer(rotationRecognizer);
        }

        private void OnPan(TKPanRecognizer recognizer)
        {
            var delta = recognizer.deltaTranslation;
            var handler = OnPanHandler;
            if (handler == null) return;
            handler(delta);
        }

        private void OnPinch(TKPinchRecognizer recognizer)
        {
            var delta = recognizer.deltaScale;
            var handler = OnPinchHandler;
            if (handler == null) return;
            handler(delta);
        }

        private void OnRotation(TKRotationRecognizer recognizer)
        {
            var delta = recognizer.deltaRotation;
            var handler = OnRotationHandler;
            if (handler == null) return;
            handler(delta);
        }

        public void Dispose()
        {
            TouchKit.removeAllGestureRecognizers();
        }
    }
}

