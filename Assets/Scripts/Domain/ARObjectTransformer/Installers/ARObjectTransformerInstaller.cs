using System;
using Zenject;

namespace Domain.ARObjectService
{
    public class ARObjectTransformerInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IARObjectTransformer), typeof(IInitializable), typeof(ITickable), typeof(IDisposable)).To(typeof(ARObjectTransformer)).AsSingle();
        }
    }
}


