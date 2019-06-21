using UnityEngine;
using UnityEngine.Events;

namespace TweenService
{
    public class TweenCanvasGroup : Tweenable
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        [Range (0, 1)]
        [SerializeField]
        private float endPoint;

        protected override void OnFinished(Vector4 endPoint)
        {
            canvasGroup.alpha = endPoint.x;
        }

        protected override void OnProcess(Vector4 current)
        {
            canvasGroup.alpha = current.x;       
        }

        protected override void OnStartTween()
        {
            Tween.TweenVector4(
                processor: this,
                startPoint: Vector4.one * canvasGroup.alpha,
                endPoint: Vector4.one * endPoint,
                time: Time,
                delay: Delay,
                curve: curve);
        }
    }
}

