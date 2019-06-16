using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Infrastructure.InputService
{
    public class InputHandlerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputSystem>().To(typeof(TouchKitInputSystem)).AsSingle();
            Container.Bind(typeof(IInputHandler), typeof(IDisposable)).To(typeof(InputHandler)).AsSingle();
        }
    }
}

