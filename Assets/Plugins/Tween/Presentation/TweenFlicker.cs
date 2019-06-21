using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TweenService
{
    public class TweenFlicker : MonoBehaviour
    {
        public bool Tweening { get; private set; }

        private Coroutine finishActionCoroutine;
        private Coroutine flickerOffCoroutine;
        [SerializeField]
        private Tweenable onTween;
        [SerializeField]
        private Tweenable offTween;
        [SerializeField]
        private UnityEvent onAction;
        [SerializeField]
        private UnityEvent finishAction;
        [SerializeField]
        private bool startOn;

        private void Start()
        {
            if (!startOn)
            {
                return;
            }

            Flicker();
        }

        public void Flicker()
        {
            Tweening = true;

            if (onAction != null)
            {
                onAction.Invoke();
            }

            StopTweenCoroutine(flickerOffCoroutine);
            StopTweenCoroutine(finishActionCoroutine);
            onTween.StartTween();
            flickerOffCoroutine = StartCoroutine(FlickerOff(onTween.TotalAnimationTime));
        }

        private IEnumerator FlickerOff(float delay)
        {
            yield return new WaitForSeconds(delay);
            offTween.StartTween();
            finishActionCoroutine = StartCoroutine(FinishAction(offTween.TotalAnimationTime));
        }

        private IEnumerator FinishAction(float delay)
        {
            yield return new WaitForSeconds(delay);

            Tweening = false;

            if (finishAction == null)
            {
                yield break;
            }

            finishAction.Invoke();
        }

        private void StopTweenCoroutine(Coroutine coroutine)
        {
            if (coroutine == null)
            {
                return;
            }

            StopCoroutine(coroutine);
        }
    }
}

