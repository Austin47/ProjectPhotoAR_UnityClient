using System;
using Infrastructure.CCSystem;
using Infrastructure.DatabaseService;
using Infrastructure.PermissionService;
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
        private IPermissions permissions;

        [Inject]
        public void Construct(
            ICameraCaptureSystem cameraCaptureSystem,
            IDatabase database,
            IPermissions permissions)
        {
            this.cameraCaptureSystem = cameraCaptureSystem;
            this.database = database;
            this.permissions = permissions;
        }

        public void CapturePhoto()
        {
            permissions.CheckWriteStorage();
            cameraCaptureSystem.CapturePhoto(SavePhotoPath);
        }
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


