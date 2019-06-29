using Domain.ARObjectSpawnService;
using Infrastructure.Common;
using UnityEngine;

namespace Presentation.ARObjectSpawner
{
    public class StandardARObject : MonoBehaviour, IARObject
    {
        [SerializeField]
        private MeshRenderer rend;

        public bool IsVisible { get { return rend.isVisible; } }

        public Vector3 pos { get { return transform.position; } }

        public void Configure(Vector3 pos, Texture2D texture)
        {
            transform.position = pos;
            rend.material.mainTexture = texture;

            Utils.EnvelopeToValueFromTexture2D(rend.transform, texture, 1);
            rend.transform.localPosition = new Vector3(0, rend.transform.localScale.y / 2, 0);
            FaceCamera();
        }

        public void SetY(float posY)
        {
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        }

        private void FaceCamera()
        {
            var camPos = Camera.main.transform.position;
            var lookAtPoint = new Vector3(camPos.x, transform.position.y, camPos.z);
            transform.LookAt(lookAtPoint);
        }
    }
}

