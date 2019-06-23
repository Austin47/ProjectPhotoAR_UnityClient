using UnityEngine.XR.ARFoundation;
using Zenject;

namespace Infrastructure.CCSystem
{
    public class CameraCaptureSystemInstaller : MonoInstaller
    {
        public ARCameraManager cameraManager;
        public override void InstallBindings()
        {
            Container.Bind(typeof(ARCameraManager)).FromInstance(cameraManager);
            Container.Bind(typeof(ICameraCaptureSystem)).To(typeof(CameraCaptureSystem)).AsSingle();
        }
    }
}


