using System;
using UnityEngine;

namespace Infrastructure.InputService
{
    public interface IInputSystem
    {
        bool IsTouching { get; }
        event Action<Vector2> OnPanHandler;
        event Action<float> OnPinchHandler;
        event Action<float> OnRotationHandler;
        bool GetTouchPos(out Vector2 pos);
    }
}

