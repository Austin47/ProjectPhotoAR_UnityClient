using Domain.ARObjectSpawnService;
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

            var textureSize = new Vector2(texture.width, texture.height);
            var scale = textureSize.GetEnvelopeToValue(1);
            rend.transform.localScale = new Vector3(scale.x, scale.y, 1);
            rend.transform.localPosition = new Vector3(0, scale.y / 2, 0);
        }
    }
}

