using System.Collections;
using UnityEngine;

namespace TweenService
{
    public interface ITweenable
    {
        MonoBehaviour Tweener { get; }
        IEnumerator CurrentProcess { get; set; }
        TweenSequencerType Type { get; }
        float TotalAnimationTime { get; }
        bool ActiveInHierarchy { get; }
        void StartTween();
        void StopTween();
        void Process(Vector4 current);
        void Finished(Vector4 endPoint);
    }
}
