using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Domain.InputService
{
    public class InputHandlerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputSystem>().To(typeof(TouchKitInputSystem));
            Container.Bind<IInputHandler>().To(typeof(InputHandler));
        }
    }
}

