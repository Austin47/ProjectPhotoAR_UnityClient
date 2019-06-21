using UnityEngine;

namespace TweenService
{
    public class TweenScale : Tweenable
    {
        [SerializeField]
        private Vector3 endPoint;
        [SerializeField]
        private Transform target;

        protected override void OnFinished(Vector4 endPoint)
        {
            OnProcess(endPoint);
        }

        protected override void OnProcess(Vector4 current)
        {
            if (!target)
            {
                transform.localScale = current;
                return;
            }

            target.localScale = current;
        }

        protected override void OnStartTween()
        {
            var startPoint = target ? target.localScale : transform.localScale;

            Tween.TweenVector4(
                processor: this,
                startPoint: startPoint,
                endPoint: endPoint,
                time: Time,
                delay: Delay,
                curve: curve);
        }
    }
}

