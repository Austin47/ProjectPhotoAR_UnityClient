using UnityEngine;

namespace TweenService
{
    public class TweenCameraSize : Tweenable
    {
        [SerializeField]
        private Camera myCamera;
        [SerializeField]
        private float size;

        public void SetSize(float size)
        {
            this.size = size;
        }

        protected override void OnFinished(Vector4 endPoint)
        {
            OnProcess(endPoint);
        }

        protected override void OnProcess(Vector4 current)
        {
            myCamera.orthographicSize = current.x;
        }

        protected override void OnStartTween()
        {
            var startPoint = myCamera.orthographicSize;

            Tween.TweenVector4(
                processor: this,
                startPoint: Vector4.one * startPoint,
                endPoint: Vector4.one * size,
                time: Time,
                delay: Delay,
                curve: curve);
        }
    }
}

