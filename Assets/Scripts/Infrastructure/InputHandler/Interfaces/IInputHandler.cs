using System;
using UnityEngine;

namespace Infrastructure.InputService
{
    public interface IInputHandler
    {
        event Action<Vector2> OnTap;
    }
}

