using UnityEngine;

namespace Infrastructure.CameraService
{
    public class CameraSystem : ICameraSystem
    {
        public Vector3 pos { get { return Camera.main.transform.position; } }
    }
}


