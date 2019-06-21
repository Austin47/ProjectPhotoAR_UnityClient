using UnityEngine;

namespace TweenService
{
    public class TweenRendererColor : Tweenable
    {
        [SerializeField]
        private Renderer renderer;
        [SerializeField]
        private Color endPoint;

        protected override void OnFinished(Vector4 endPoint)
        {
            renderer.material.color = endPoint;
        }

        protected override void OnProcess(Vector4 current)
        {
            renderer.material.color = current;
        }

        protected override void OnStartTween()
        {
            var startPoint = renderer.material.color;

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

