using UnityEngine;
using Zenject;

namespace Presentation.ARObjectUI
{
    public class ARObjectUIInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject prefab;

        public override void InstallBindings()
        {
            Container.BindMemoryPool<IARObjectUIBlock, ARObjectUIBlockPool>().To<ARObjectUIBlock>().FromComponentInNewPrefab(prefab);
        }
    }
}

