using UnityEngine;
using System;
using Infrastructure.InputService;
using Zenject;

namespace Domain.ARObjectSpawnService
{
    public class ARObjectSpawner : IARObjectSpawner, IInitializable, IDisposable
    {
        private ARObjectPool aRObjectPool;
        private IInputHandler inputHandler;

        [Inject]
        private void Construct(
            ARObjectPool aRObjectPool,
            IInputHandler inputHandler)
        {
            this.aRObjectPool = aRObjectPool;
            this.inputHandler = inputHandler;
        }

        public void Initialize() => inputHandler.OnTap += OnTap;
        public void Dispose() => inputHandler.OnTap -= OnTap;

        private void OnTap(Vector2 pos)
        {
            aRObjectPool.Spawn();
        }
    }
}


