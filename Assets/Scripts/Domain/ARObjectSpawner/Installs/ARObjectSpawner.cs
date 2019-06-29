using UnityEngine;
using Zenject;
using Infrastructure.RaycastService;
using Domain.ARAlignmentService;

namespace Domain.ARObjectSpawnService
{
    public class ARObjectSpawner : IARObjectSpawner
    {
        private const float SPAWN_DISTANCE = .5f;

        private IARObjectAlignment aRObjectAlignment;
        private ARObjectPool aRObjectPool;
        private IRaycastSystem raycastSystem;

        [Inject]
        private void Construct(
            IARObjectAlignment aRObjectAlignment,
            ARObjectPool aRObjectPool,
            IRaycastSystem raycastSystem)
        {
            this.aRObjectAlignment = aRObjectAlignment;
            this.aRObjectPool = aRObjectPool;
            this.raycastSystem = raycastSystem;
        }

        public void Spawn(Texture2D texture)
        {
            var spawnPoint = Vector3.zero;
            var planeFound = true;
            if (!raycastSystem.TryTouchPosToARPlane(CenterOfScreen(), out spawnPoint))
            {
                spawnPoint = Camera.main.transform.forward * SPAWN_DISTANCE;
                planeFound = false;
            }
            var clone = aRObjectPool.Spawn();
            clone.Configure(spawnPoint, texture);
            if (!planeFound)
                aRObjectAlignment.RegistererUnaligned(clone);
        }

        private Vector2 CenterOfScreen()
        {
            return new Vector2(Screen.width / 2, Screen.height / 2);
        }
    }
}


