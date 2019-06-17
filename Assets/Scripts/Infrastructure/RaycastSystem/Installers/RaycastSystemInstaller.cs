using Zenject;

namespace Infrastructure.RaycastService
{
    public class RaycastSystemInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IRaycastSystem), typeof(IInitializable)).To(typeof(RaycastSystem)).AsSingle();
        }
    }
}


