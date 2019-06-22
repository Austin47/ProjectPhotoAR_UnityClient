using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Presentation.ARObjectUI
{
    public class ARObjectUIlGrid : MonoBehaviour
    {
        [SerializeField]
        private Transform gridRoot;

        private ARObjectUIBlockPool aRObjectUIBlockPool;

        [Inject]
        private void Construct(ARObjectUIBlockPool aRObjectUIBlockPool)
        {
            this.aRObjectUIBlockPool = aRObjectUIBlockPool;
        }

        private void Start()
        {
            var clone = aRObjectUIBlockPool.Spawn();
            clone.Configure(gridRoot);
        }
    }
}
