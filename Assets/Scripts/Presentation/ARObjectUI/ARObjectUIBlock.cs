using Domain.ARObjectDatabaseService;
using Infrastructure.DatabaseService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presentation.ARObjectUI
{
    public class ARObjectUIBlock : MonoBehaviour, IARObjectUIBlock
    {
        [SerializeField]
        private RawImage image;

        private IDatabase database;

        [Inject]
        private void Construct(IDatabase database)
        {
            this.database = database;
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
        }
    }
}

