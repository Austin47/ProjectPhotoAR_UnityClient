using Domain.ARObjectSpawnService;
using Infrastructure.Common;
using UnityEngine;

namespace Presentation.ARObjectSpawner
{
    public class StandardARObject : MonoBehaviour, IARObject
    {
        [SerializeField]
        private MeshRenderer rend;

        public void Configure(Vector3 pos, Texture2D texture)
        {
            transform.position = pos;
            rend.material.mainTexture = texture;

            Utils.EnvelopeToValueFromTexture2D(rend.transform, texture, 1);
        }
    }
}

