using UnityEngine;

namespace Infrastructure.RaycastService
{
    public interface IRaycastSystem
    {
        bool TryTouchPosToARPlane(Vector2 pos, out Vector3 spawnPoint);
    }
}


