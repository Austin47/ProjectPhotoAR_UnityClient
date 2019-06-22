using UnityEngine;

namespace Domain.ARObjectSpawnService
{
    public interface IARObject
    {
        void Configure(Vector3 pos, Texture2D text);
    }
}

