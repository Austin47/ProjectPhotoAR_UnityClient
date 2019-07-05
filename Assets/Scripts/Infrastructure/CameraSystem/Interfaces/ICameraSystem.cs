using UnityEngine;

namespace Infrastructure.CameraService
{
    public interface ICameraSystem
    {
        Vector3 Pos { get; }
        Vector3 GetPointInFrontOfCamera(float distance);
        Vector3 ScreenToWorldPoint(Vector3 point);
    }
}


