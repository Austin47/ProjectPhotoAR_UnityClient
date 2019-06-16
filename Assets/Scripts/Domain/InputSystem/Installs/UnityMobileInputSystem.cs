using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.InputService
{
    public class UnityMobileInputSystem : IInputSystem
    {
        public bool IsTouching => throw new System.NotImplementedException();

        public Vector2 Touch1Pos => throw new System.NotImplementedException();
    }
}

