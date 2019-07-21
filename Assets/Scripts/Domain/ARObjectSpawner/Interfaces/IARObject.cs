using UnityEngine;

namespace Domain.ARObjectSpawnService
{
    public interface IARObject
    {
        bool IsVisible { get; }
        Vector3 Pos { get; }
        Vector3 Scale { get; }
        void Configure(Vector3 pos, Texture2D text);
        void SetPosition(Vector3 pos);
        void SetScale(Vector3 scale);
        void RotateZ(float delta);
    }
}

