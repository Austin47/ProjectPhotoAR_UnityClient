using UnityEngine;

namespace Infrastructure.CameraService
{
    public class CameraSystem : ICameraSystem
    {
        private Camera cam;
        private Camera Cam
        {
            get
            {
                if (cam == null) cam = Camera.main;
                return cam;
            }
        }

        public Vector3 Pos { get { return Cam.transform.position; } }

        public Vector3 GetPointInFrontOfCamera(float distance)
        {
            return (Camera.main.transform.position + Camera.main.transform.forward) * distance;
        }

        public Vector3 ScreenToWorldPoint(Vector3 point)
        {
            return Cam.ScreenToWorldPoint(point);
        }
    }
}


