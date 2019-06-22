using System.Collections;
using System.Collections.Generic;
using Domain.ARObjectDatabaseService;
using UnityEngine;
using Zenject;

namespace Presentation.ARObjectUI
{
    public class ARObjectUIlGrid : MonoBehaviour
    {
        [SerializeField]
        private Transform gridRoot;

        private IARObjectDatabase aRObjectDatabase;
        private ARObjectUIBlockPool aRObjectUIBlockPool;

        [Inject]
        private void Construct(
            ARObjectUIBlockPool aRObjectUIBlockPool,
            IARObjectDatabase aRObjectDatabase)
        {
            this.aRObjectUIBlockPool = aRObjectUIBlockPool;
            this.aRObjectDatabase = aRObjectDatabase;
        }

        private IEnumerator Start()
        {
            while(aRObjectDatabase.DefaultARObjects == null) yield return null;

            foreach(ARObjectData data in aRObjectDatabase.DefaultARObjects)
            {
                var clone = aRObjectUIBlockPool.Spawn();
                clone.Configure(gridRoot, data);
            }
        }
    }
}
