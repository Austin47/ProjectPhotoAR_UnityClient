using UnityEngine;

namespace Infrastructure.CameraService
{
    public class CameraSystem : ICameraSystem
    {
        public Vector3 cameraPos { get { return Camera.main.transform.position; } }
    }
}


