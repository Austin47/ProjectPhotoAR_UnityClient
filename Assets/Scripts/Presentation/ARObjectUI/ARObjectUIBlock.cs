using Domain.ARObjectDatabaseService;
using Domain.ARObjectSpawnService;
using Domain.LayoutHandlerService;
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

        private IARObjectSpawner aRObjectSpawner;
        private IDatabase database;
        private ILayoutHandler layoutHandler;

        [Inject]
        private void Construct(
            IARObjectSpawner aRObjectSpawner,
            IDatabase database,
            ILayoutHandler layoutHandler)
        {
            this.aRObjectSpawner = aRObjectSpawner;
            this.database = database;
            this.layoutHandler = layoutHandler;
        }

        private void Awake()
        {
            defaultImageSize = image.rectTransform.rect.width;
        }

        public void Configure(Transform root, ARObjectData data)
        {
            transform.SetParent(root);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
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

        public void SpawnObject()
        {
            aRObjectSpawner.Spawn((Texture2D) image.texture);
            layoutHandler.GoBack();
        }
    }
}

