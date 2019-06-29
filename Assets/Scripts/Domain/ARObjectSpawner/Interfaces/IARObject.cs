using UnityEngine;

namespace Domain.ARObjectSpawnService
{
    public interface IARObject
    {
        bool IsVisible { get; }
        Vector3 pos { get; }
        void Configure(Vector3 pos, Texture2D text);
        void SetPosition(Vector3 pos);
    }
}

