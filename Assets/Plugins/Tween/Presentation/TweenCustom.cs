using UnityEngine;
using UnityEngine.Events;

namespace TweenService
{
    public class TweenCustom : Tweenable
    {
        [SerializeField]
        private UnityEvent onStart;
        [SerializeField]
        private UnityEvent onProcess;
        [SerializeField]
        private UnityEvent onFinished;

        protected override void OnStartTween()
        {
            FireAction(onStart);
            Tween.TweenVector4(
                processor: this,
                startPoint: Vector4.zero,
                endPoint: Vector4.one,
                time: Time,
                delay: Delay,
                curve: curve);
        }

        protected override void OnProcess(Vector4 current)
        {
            FireAction(onProcess);
        }

        protected override void OnFinished(Vector4 endPoint)
        {
            FireAction(onFinished);
        }

        private void FireAction(UnityEvent action)
        {
            if(action == null)
            {
                return;
            }

            action.Invoke();
        }
    }
}
