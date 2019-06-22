using UnityEngine;
using System;
using Infrastructure.InputService;
using Zenject;
using Infrastructure.RaycastService;

namespace Domain.ARObjectSpawnService
{
    public class ARObjectSpawner : IARObjectSpawner, IInitializable, IDisposable
    {
        private ARObjectPool aRObjectPool;
        private IInputHandler inputHandler;
        private IRaycastSystem raycastSystem;

        [Inject]
        private void Construct(
            ARObjectPool aRObjectPool,
            IInputHandler inputHandler,
            IRaycastSystem raycastSystem)
        {
            this.aRObjectPool = aRObjectPool;
            this.inputHandler = inputHandler;
            this.raycastSystem = raycastSystem;
        }

        public void Initialize() 
        {
            inputHandler.OnTap += OnTap;
        }
        public void Dispose() => inputHandler.OnTap -= OnTap;

        private void OnTap(Vector2 pos)
        {
            Vector3 spawnPoint = new Vector3();
            if(!raycastSystem.TryTouchPosToARPlane(pos, out spawnPoint)) return;
            var clone = aRObjectPool.Spawn();
            //clone.Configure(spawnPoint);
        }
    }
}


