
using System.Collections;
using UnityEngine;

namespace Infrastructure.CoroutineRunner
{
    public class CoroutineRunner : ICoroutineRunner
    {
        internal class Runner : MonoBehaviour
        {
            internal void Awake()
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        private Runner runner;
        private Runner CurrentRunner
        {
            get
            {
                if (runner == null)
                {
                    runner = new GameObject("CoroutineRunner").AddComponent<Runner>();

                }

                return runner;
            }
        }

        public void RunCoroutine(IEnumerator coroutine)
        {
            CurrentRunner.StartCoroutine(coroutine);
        }
    }
}


