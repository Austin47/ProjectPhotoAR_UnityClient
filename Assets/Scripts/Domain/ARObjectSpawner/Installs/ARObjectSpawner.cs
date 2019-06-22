using UnityEngine;
using Infrastructure.InputService;
using Zenject;
using Infrastructure.RaycastService;

namespace Domain.ARObjectSpawnService
{
    public class ARObjectSpawner : IARObjectSpawner
    {
        private const float SPAWN_DISTANCE = .5f;

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
            Vector3 spawnPoint = Vector3.zero;
            if (!raycastSystem.TryTouchPosToARPlane(CenterOfScreen(), out spawnPoint))
                spawnPoint = Camera.main.transform.forward * SPAWN_DISTANCE;
            var clone = aRObjectPool.Spawn();
            clone.Configure(spawnPoint, texture);
        }

        private Vector2 CenterOfScreen()
        {
            return new Vector2(Screen.width / 2, Screen.height / 2);
        }
    }
}


