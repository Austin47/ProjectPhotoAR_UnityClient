using System.Collections.Generic;
using Domain.ARObjectSpawnService;
using Infrastructure.CameraService;
using Infrastructure.Common;
using Infrastructure.RaycastService;
using UnityEngine;
using Zenject;

namespace Domain.ARAlignmentService
{
    public class ARObjectAlignment : IARObjectAlignment, ITickable
    {
        private const int MAX_ALIGNMENTS_PER_TICK = 5;

        private List<IARObject> unaligned = new List<IARObject>();

        private ICameraSystem cameraSystem;
        private IRaycastSystem raycastSystem;

        [Inject]
        private void Construct(
            ICameraSystem cameraSystem,
            IRaycastSystem raycastSystem)
        {
            this.cameraSystem = cameraSystem;
            this.raycastSystem = raycastSystem;
        }

        public void Tick()
        {
            for(int i = 0; i < unaligned.Count; i++)
            {
                Align(unaligned[i]);
            }
        }

        private void Align(IARObject arObject)
        {
            if(!arObject.IsVisible) return;
            var direction = Utils.GetDirectionBetweenVectors(cameraSystem.pos, arObject.pos);
            var newPos = Vector3.zero;
            if (!raycastSystem.TryToGetPlanePoint(cameraSystem.pos, direction, out newPos)) return;
            arObject.SetPosition(newPos);
            unaligned.Remove(arObject);
        }

        public void RegistererUnaligned(IARObject aRObject)
        {
            if(unaligned.Contains(aRObject)) return;
            unaligned.Add(aRObject);
        }
    }
}


