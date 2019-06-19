using UnityEngine;

namespace PhotoGalleryService
{
    internal class AndroidCallback : AndroidJavaProxy
    {
        public AndroidCallback() : base("com.absurd.photogallery.PickPhotoResults") { }
        public bool IsFinished { get; private set; }
        public string Result { get; private set; }

        public void OnFinished(string result)
        {
            Result = result;
            IsFinished = true;
        }
    }
}

