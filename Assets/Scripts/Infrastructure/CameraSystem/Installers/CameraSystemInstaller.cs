using Zenject;

namespace Infrastructure.CameraService
{
    public class CameraSystemInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ICameraSystem)).To(typeof(CameraSystem)).AsSingle();
        }
    }
}


