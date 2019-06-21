using UnityEngine;

namespace TweenService
{
    public class TweenColorMaterialProperty : Tweenable
    {
        [SerializeField]
        private Renderer renderer;
        [SerializeField]
        private string propertyName = "_EmissionColor";
        [SerializeField]
        private Color endPoint;

        protected override void OnFinished(Vector4 endPoint)
        {
            OnProcess(endPoint);
        }

        protected override void OnProcess(Vector4 current)
        {
            renderer.material.SetColor(propertyName, current);
        }

        protected override void OnStartTween()
        {
            var startPoint = renderer.material.GetColor(propertyName);

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

