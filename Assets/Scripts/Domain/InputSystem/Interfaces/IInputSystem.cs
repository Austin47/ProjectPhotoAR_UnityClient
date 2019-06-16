using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.InputService
{

    public interface IInputSystem
    {
        bool IsTouching { get; }
        Vector2 Touch1Pos { get; }
    }
}

