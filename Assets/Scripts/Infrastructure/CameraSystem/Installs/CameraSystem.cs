using UnityEngine;

namespace Infrastructure.CameraService
{
    public class CameraSystem : ICameraSystem
    {
        public Vector3 pos { get { return Camera.main.transform.position; } }

        public Vector3 GetPointInFrontOfCamera(float distance)
        {
            return (Camera.main.transform.position + Camera.main.transform.forward) * distance;
        }
    }
}


