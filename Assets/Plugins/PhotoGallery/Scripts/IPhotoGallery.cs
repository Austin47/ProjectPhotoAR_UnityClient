using System;
using UnityEngine;

namespace PhotoGalleryService
{
    public interface IPhotoGallery
    {
        void SelectPhotoPath(Action<string> callback);
        void LoadPhoto(string uri, Action<Texture2D> callback);
    }
}

