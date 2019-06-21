using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TweenService
{
    public class TweenColor : Tweenable
    {
        [SerializeField]
        private MaskableGraphic image;
        [SerializeField]
        private Color endPoint;

        protected override void OnFinished(Vector4 endPoint)
        {
            image.color = endPoint;
        }

        protected override void OnProcess(Vector4 current)
        {
            image.color = current;
        }

        protected override void OnStartTween()
        {
            var startPoint = image.color;

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


