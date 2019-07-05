using UnityEngine;

namespace Infrastructure.CameraService
{
    public interface ICameraSystem
    {
        Camera Cam { get; }
        Vector3 Pos { get; }
        Vector3 GetPointInFrontOfCamera(float distance);
    }
}


