using Zenject;

namespace Domain.ARAlignmentService
{
    public class ARObjectAlignmentInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IARObjectAlignment), typeof(ITickable)).To(typeof(ARObjectAlignment)).AsSingle();
        }
    }
}


