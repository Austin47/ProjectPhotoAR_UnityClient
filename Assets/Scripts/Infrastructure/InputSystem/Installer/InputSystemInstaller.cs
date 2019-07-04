using System;
using Zenject;

namespace Infrastructure.InputService
{
    public class InputSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInputSystem), typeof(IDisposable)).To(typeof(TouchKitInputSystem)).AsSingle();
        }
    }
}

