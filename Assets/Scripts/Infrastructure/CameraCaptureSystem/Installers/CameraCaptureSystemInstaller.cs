using UnityEngine;
using Zenject;

namespace Infrastructure.CCSystem
{
    public class CameraCaptureSystemInstaller : MonoInstaller
    {
        public Canvas canvas;

        public override void InstallBindings()
        {
            Container.Bind(typeof(Canvas)).FromInstance(canvas);
            Container.Bind(typeof(ICameraCaptureSystem)).To(typeof(CameraCaptureSystem)).AsSingle();
        }
    }
}


