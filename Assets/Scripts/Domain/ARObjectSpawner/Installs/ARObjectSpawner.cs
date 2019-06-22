using UnityEngine;
using Infrastructure.InputService;
using Zenject;
using Infrastructure.RaycastService;

namespace Domain.ARObjectSpawnService
{
    public class ARObjectSpawner : IARObjectSpawner
    {
        private const int SPAWN_DISTANCE = 2;

        private ARObjectPool aRObjectPool;
        private IRaycastSystem raycastSystem;

        [Inject]
        private void Construct(
            ARObjectPool aRObjectPool,
            IRaycastSystem raycastSystem)
        {
            this.aRObjectPool = aRObjectPool;
            this.raycastSystem = raycastSystem;
        }

        public void Spawn(Texture2D texture)
        {
            Vector3 spawnPoint = Camera.main.transform.forward * SPAWN_DISTANCE;
            //if(!raycastSystem.TryTouchPosToARPlane(new Vector2(), out spawnPoint)) return;
            var clone = aRObjectPool.Spawn();
            clone.Configure(spawnPoint, texture);
        }
    }
}


