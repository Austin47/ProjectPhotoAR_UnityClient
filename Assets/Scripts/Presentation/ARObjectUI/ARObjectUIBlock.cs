using Domain.ARObjectDatabaseService;
using Domain.ARObjectSpawnService;
using Domain.LayoutHandlerService;
using Infrastructure.Common;
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
            // TODO: AT - logic for when its not from the streaming assets
            database.LoadTextureFromStringmAssets(data.path, OnLoadDefaultTexture);
        }

        public void OnLoadDefaultTexture(Texture2D texture)
        {
            image.texture = texture;
            Utils.EnvelopeToValueFromTexture2D(image, texture, defaultImageSize);
        }

        public void SpawnObject()
        {
            aRObjectSpawner.Spawn((Texture2D) image.texture);
            layoutHandler.GoBack();
        }
    }
}

