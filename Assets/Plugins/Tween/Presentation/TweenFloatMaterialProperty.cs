using UnityEngine;

namespace TweenService
{
    public class TweenFloatMaterialProperty : Tweenable
    {
        [SerializeField]
        private Renderer renderer;
        [SerializeField]
        private string propertyName = "_Metallic";
        [SerializeField]
        private float endPoint;

        protected override void OnFinished(Vector4 endPoint)
        {
            OnProcess(endPoint);
        }

        protected override void OnProcess(Vector4 current)
        {
            renderer.material.SetFloat(propertyName, current.x);
        }

        protected override void OnStartTween()
        {
            var startPoint = renderer.material.GetFloat(propertyName);

            Tween.TweenVector4(
                processor: this,
                startPoint: new Vector4(startPoint, startPoint, startPoint, startPoint),
                endPoint: new Vector4(endPoint, endPoint, endPoint, endPoint),
                time: Time,
                delay: Delay,
                curve: curve);
        }
    }
}

