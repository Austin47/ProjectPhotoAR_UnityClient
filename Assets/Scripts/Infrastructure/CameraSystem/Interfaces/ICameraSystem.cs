using UnityEngine;

namespace Infrastructure.CameraService
{
    public interface ICameraSystem
    {
        Vector3 pos { get; }
        Vector3 GetPointInFrontOfCamera(float distance);
    }
}


