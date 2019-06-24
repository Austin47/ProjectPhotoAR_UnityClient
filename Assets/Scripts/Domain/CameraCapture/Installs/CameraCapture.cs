using System;
using Infrastructure.CCSystem;
using Infrastructure.DatabaseService;
using PhotoGalleryService;
using UnityEngine;
using Zenject;

namespace Domain.CameraCaptureService
{
    public class CameraCapture
    {
        public event Action<Texture2D> OnCapturePhoto;

        private ICameraCaptureSystem cameraCaptureSystem;
        private IDatabase database;

        [Inject]
        public void Construct(
            ICameraCaptureSystem cameraCaptureSystem,
            IDatabase database)
        {
            this.cameraCaptureSystem = cameraCaptureSystem;
            this.database = database;
        }

        public void CapturePhoto() => cameraCaptureSystem.CapturePhoto(SavePhotoPath);
        private void SavePhotoPath(string photo)
        {
            database.SavePathToLocal(photo);
            database.LoadTextureFromLocalApp(photo, OnGetSavedPhoto);
        }

        private void OnGetSavedPhoto(Texture2D texture)
        {
            var handler = OnCapturePhoto;
            if (handler != null) handler(texture);
        }
    }
}


