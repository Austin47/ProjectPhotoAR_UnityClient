using System;
using Presentation.ARObjectSpawner;
using UnityEngine;
using Zenject;

namespace Domain.ARObjectSpawnService
{
    public class ARObjectSpawnerInstaller : MonoInstaller 
    {
        [SerializeField]
        private GameObject prefab;

        public override void InstallBindings()
        {
            Container.Bind(typeof(IARObjectSpawner)).To(typeof(ARObjectSpawner)).AsSingle();
            // TODO: AT - we generally never want to access a Presentation element in the domain, possibly find a way around this
            // Swap out StandardARObject to spawn a different type
            Container.BindMemoryPool<IARObject, ARObjectPool>().To<StandardARObject>().FromComponentInNewPrefab(prefab);
        }
    }
}


