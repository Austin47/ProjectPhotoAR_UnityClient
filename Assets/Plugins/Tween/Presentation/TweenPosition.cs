using UnityEngine;

namespace TweenService
{
    public class TweenPosition : Tweenable
    {
        [SerializeField]
        private bool useLocalPosition;
        [SerializeField]
        private bool useExplicitEndPoint;
        [SerializeField]
        private Transform endPoint;
        [SerializeField]
        private Vector3 explicitEndPoint;

        protected override void OnStartTween()
        {
            var end = new Vector3();
            if (useExplicitEndPoint)
            {
                end = explicitEndPoint;
            }
            else
            {
                end = useLocalPosition ? endPoint.localPosition : endPoint.position;
            }

            var startPoint = useLocalPosition ? transform.localPosition : transform.position;

            Tween.TweenVector4(
                processor: this,
                startPoint: startPoint,
                endPoint: end,
                time: Time,
                delay: Delay,
                curve: curve);
        }

        protected override void OnProcess(Vector4 current)
        {
            if (useLocalPosition)
            {
                transform.localPosition = current;
                return;
            }

            transform.position = current;
        }

        protected override void OnFinished(Vector4 endPoint)
        {
            if (useLocalPosition)
            {
                transform.localPosition = endPoint;
                return;
            }

            transform.position = endPoint;
        }

        public void SetExplicitEndPoint(Vector3 explicitEndPoint)
        {
            this.explicitEndPoint = explicitEndPoint;
        }
    }
}

