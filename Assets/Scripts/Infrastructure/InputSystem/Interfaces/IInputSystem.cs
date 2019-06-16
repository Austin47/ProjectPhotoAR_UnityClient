using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.InputService
{
    public interface IInputSystem
    {
        event Action<Vector2> OnTap;
    }
}

