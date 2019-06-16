using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.InputService
{
    public interface IInputHandler
    {
        event Action<Vector2> OnTap;
    }
}

