using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TweenService
{
    public class TweenScrollRect : Tweenable
    {
        [SerializeField]
        private ScrollRect scrollRect;
        [SerializeField]
        private Vector2 endPoint;

        protected override void OnFinished(Vector4 endPoint)
        {
            scrollRect.normalizedPosition = endPoint;
        }

        protected override void OnProcess(Vector4 current)
        {
            scrollRect.normalizedPosition = current;
        }

        protected override void OnStartTween()
        {
            Tween.TweenVector4(
                processor: this,
                startPoint: scrollRect.normalizedPosition,
                endPoint: endPoint,
                time: Time,
                delay: Delay,
                curve: curve);
        }
    }
}

