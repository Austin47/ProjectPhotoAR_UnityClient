using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace Infrastructure.CCSystem
{
    public class CameraCaptureSystemInstaller : MonoInstaller
    {
        public ARCameraBackground aRCameraBackground;
        public RenderTexture renderTexture;
        public Canvas canvas;

        public override void InstallBindings()
        {
            Container.Bind(typeof(ARCameraBackground)).FromInstance(aRCameraBackground);
            Container.Bind(typeof(Canvas)).FromInstance(canvas);
            Container.Bind(typeof(RenderTexture)).FromInstance(renderTexture);
            Container.Bind(typeof(ICameraCaptureSystem)).To(typeof(CameraCaptureSystem)).AsSingle();
        }
    }
}


