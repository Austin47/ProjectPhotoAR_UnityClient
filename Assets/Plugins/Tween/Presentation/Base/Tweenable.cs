
using System.Collections;
using UnityEngine;

namespace TweenService
{
    abstract public class Tweenable : MonoBehaviour, ITweenable
    {
        public MonoBehaviour Tweener { get { return this; } }
        public IEnumerator CurrentProcess { get; set; }
        public TweenSequencerType Type { get { return sequencerType;} }
        public float TotalAnimationTime { get { return Delay + Time; } }
        public bool ActiveInHierarchy { get { return gameObject.activeInHierarchy; } }
        protected float Time { get { return Random.Range(timeMin, timeMax); } }
        protected float Delay { get { return Random.Range(delayMin, delayMax); } }

        [SerializeField]
        private TweenSequencerType sequencerType;
        [SerializeField]
        private float delayMin;
        [SerializeField]
        private float delayMax;
        [SerializeField]
        private float timeMin = 1f;
        [SerializeField]
        private float timeMax = 1f;
        [SerializeField]
        protected AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

        public void Process(Vector4 current)
        {
            OnProcess(current);
        }

        public void Finished(Vector4 endPoint)
        {
            OnFinished(endPoint);
        }

        public void StartTween()
        {
            OnStartTween();
        }

        public void StopTween()
        {
            if(CurrentProcess == null)
            {
                return;
            }

            StopCoroutine(CurrentProcess);
        }

        public void SetTime(float time)
        {
            timeMin = time;
            timeMax = time;
        }

        protected abstract void OnStartTween();
        protected abstract void OnProcess(Vector4 current);
        protected abstract void OnFinished(Vector4 endPoint);
    }
}




