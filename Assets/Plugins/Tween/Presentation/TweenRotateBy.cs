using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TweenService
{
    public class TweenRotateBy : Tweenable
    {
        private Vector4 currentCache;

        [SerializeField]
        private bool useLocalRotation;
        [SerializeField]
        private Vector3 rotateBy;

        protected override void OnStartTween()
        {
            currentCache = new Vector4();
            var start = useLocalRotation ? transform.localEulerAngles : transform.eulerAngles;
            var end = start + rotateBy;

            Tween.TweenVector4(
                processor: this,
                startPoint: start,
                endPoint: end,
                time: Time,
                delay: Delay,
                curve: curve);
        }

        protected override void OnProcess(Vector4 current)
        {
            var stepSize = current - currentCache;
            currentCache = current;
            var space = useLocalRotation ? Space.Self : Space.World;
            transform.Rotate(stepSize, space);
        }

        protected override void OnFinished(Vector4 endPoint)
        {
            OnProcess(endPoint);
            var angleX = Mathf.RoundToInt(transform.eulerAngles.x);
            var angleY = Mathf.RoundToInt(transform.eulerAngles.y);
            var angleZ = Mathf.RoundToInt(transform.eulerAngles.z);
            transform.eulerAngles = new Vector3(angleX, angleY, angleZ);
        }

        public void SetRotateBy(Vector3 rotateBy)
        {
            this.rotateBy = rotateBy;
        }
    }
}

