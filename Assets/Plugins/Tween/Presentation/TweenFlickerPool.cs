using System.Collections.Generic;
using UnityEngine;

namespace TweenService
{
    public class TweenFlickerPool : MonoBehaviour
    {
        [SerializeField]
        private List<TweenFlicker> tweenFlickers = new List<TweenFlicker>();

        public void Flicker()
        {
            for (int i = 0; i < tweenFlickers.Count; i++)
            {
                if (tweenFlickers[i].Tweening)
                {
                    continue;
                }

                tweenFlickers[i].Flicker();
                return;
            }
        }
    }
}

