using Domain.ARObjectDatabaseService;
using Infrastructure.DatabaseService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presentation.ARObjectUI
{
    public class ARObjectUIBlock : MonoBehaviour, IARObjectUIBlock
    {
        private float defaultImageSize = 425;

        [SerializeField]
        private RawImage image;

        private IDatabase database;

        [Inject]
        private void Construct(IDatabase database)
        {
            this.database = database;
        }

        private void Awake()
        {
            //defaultImageSize = image.rectTransform.sizeDelta.x;
        }

        public void Configure(Transform root, ARObjectData data)
        {
            transform.SetParent(root);
            transform.localScale = Vector3.one;
            database.LoadDefaultTexture(data.path, OnLoadDefaultTexture);
        }

        public void OnLoadDefaultTexture(Texture2D texture)
        {
            image.texture = texture;
            ResizeImageBasedOnTexture(texture);
        }

        private void ResizeImageBasedOnTexture(Texture2D texture)
        {
            var textureSize = new Vector2(texture.width, texture.height);
            var size = textureSize.GetEnvelopeToValue(defaultImageSize);
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }
    }
}

