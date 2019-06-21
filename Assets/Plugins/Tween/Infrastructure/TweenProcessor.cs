using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TweenService
{
    internal class TweenProcessor
    {
        public void Process(ITweenable processor, Vector4 startPoint, Vector4 endPoint, float time, float delay, AnimationCurve curve)
        {
            StopProcess(processor);
            SetProcess(processor, Processor(processor, startPoint, endPoint, time, delay, curve));
            StartProcess(processor);
        }

        private IEnumerator Processor(ITweenable processor, Vector4 startPoint, Vector4 endPoint, float time, float delay, AnimationCurve curve)
        {
            yield return new WaitForSeconds(delay);

            var delta = 0f;
            var current = Vector4.zero;
            while(delta < 1)
            {
                current = startPoint + ((endPoint - startPoint) * GetDelta(delta, curve));
                processor.Process(current);
                delta += Time.deltaTime / time;
                yield return null;
            }

            processor.Finished(endPoint);

            yield return null;
        }

        private float GetDelta(float delta, AnimationCurve curve)
        {
            return curve.Evaluate(delta);
        }

        private void SetProcess(ITweenable processor, IEnumerator process)
        {
            processor.CurrentProcess = process;
        }

        private void StartProcess(ITweenable processor)
        {
            if(!processor.ActiveInHierarchy)
            {
                return;
            }

            processor.Tweener.StartCoroutine(processor.CurrentProcess);
        }

        private void StopProcess(ITweenable processor)
        {
            if(processor.CurrentProcess == null)
            {
                return;
            }

            processor.Tweener.StopCoroutine(processor.CurrentProcess);
        }
    }
}

