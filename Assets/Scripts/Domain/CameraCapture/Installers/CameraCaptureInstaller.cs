using Zenject;

namespace Domain.CameraCaptureService
{
    public class CameraCaptureInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(CameraCapture)).AsSingle();
        }
    }
}


