using UnityEngine;

namespace Infrastructure.RaycastService
{
    public interface IRaycastSystem
    {
        bool TryTouchPosToARPlane(Vector2 pos, out Vector3 spawnPoint);
        bool TryToGetPlanePoint(Vector3 start, Vector3 direction, out Vector3 planePoint);
    }
}


