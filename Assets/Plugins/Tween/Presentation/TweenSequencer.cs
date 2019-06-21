using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TweenService
{
    public class TweenSequencer : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField]
        private KeyCode toggleOn = KeyCode.UpArrow;
        [SerializeField]
        private KeyCode toggleOff = KeyCode.DownArrow;
#endif
        [SerializeField]
        private bool tooggleOnStart;
        [SerializeField]
        private UnityEvent onAction;
        [SerializeField]
        private UnityEvent onActionAfterAnimation;
        [SerializeField]
        private UnityEvent offAction;

        private Coroutine offCoroutine;
        private Coroutine onActionAfterCorutine;
        private ITweenable[] tweenables;

        private void Awake()
        {
            tweenables = GetComponentsInChildren<ITweenable>(true);

            if (!tooggleOnStart)
            {
                return;
            }

            ToggleOn();
        }

        public void ToggleOn()
        {
            StopOffAction();
            StopOnActionAfterAnimation();
            OnAction();
            var totalAnimationTime = 0f;
            for (int i = 0; i < tweenables.Length; i++)
            {
                switch (tweenables[i].Type)
                {
                    case TweenSequencerType.On:
                        tweenables[i].StartTween();
                        var longerAnimationTime = tweenables[i].TotalAnimationTime > totalAnimationTime;
                        totalAnimationTime = longerAnimationTime ? tweenables[i].TotalAnimationTime : totalAnimationTime;
                        break;

                    case TweenSequencerType.Off:
                        tweenables[i].StopTween();
                        break;
                }
            }

            onActionAfterCorutine = StartCoroutine(OnActionAfterAnimation(totalAnimationTime));
        }

        public void ToggleOff()
        {
            StopOffAction();
            StopOnActionAfterAnimation();
            var totalAnimationTime = 0f;
            for (int i = 0; i < tweenables.Length; i++)
            {
                switch (tweenables[i].Type)
                {
                    case TweenSequencerType.On:
                        tweenables[i].StopTween();
                        break;

                    case TweenSequencerType.Off:
                        tweenables[i].StartTween();
                        var longerAnimationTime = tweenables[i].TotalAnimationTime > totalAnimationTime;
                        totalAnimationTime = longerAnimationTime ? tweenables[i].TotalAnimationTime : totalAnimationTime;
                        break;
                }
            }
            offCoroutine = StartCoroutine(OffAction(totalAnimationTime));
        }

        private void OnAction()
        {
            if (onAction == null)
            {
                return;
            }

            onAction.Invoke();
        }

        private IEnumerator OnActionAfterAnimation(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (onActionAfterAnimation == null)
            {
                yield break;
            }

            onActionAfterAnimation.Invoke();
        }

        private IEnumerator OffAction(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (offAction == null)
            {
                yield break;
            }

            offAction.Invoke();
        }

        private void StopOffAction()
        {
            if (offCoroutine == null)
            {
                return;
            }

            StopCoroutine(offCoroutine);
        }

        private void StopOnActionAfterAnimation()
        {
            if (onActionAfterCorutine == null)
            {
                return;
            }

            StopCoroutine(onActionAfterCorutine);
        }

#if UNITY_EDITOR

        private void Update()
        {
            if (UnityEditor.Selection.activeObject != gameObject)
            {
                return;
            }

            if (Input.GetKeyDown(toggleOn))
            {
                ToggleOn();
            }

            if (Input.GetKeyDown(toggleOff))
            {
                ToggleOff();
            }
        }

#endif
    }
}

