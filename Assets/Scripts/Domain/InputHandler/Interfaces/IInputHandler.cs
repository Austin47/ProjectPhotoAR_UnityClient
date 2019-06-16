using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.InputService
{
    public interface IInputHandler
    {
        event Action<Vector2> OnTouch;
    }
}

