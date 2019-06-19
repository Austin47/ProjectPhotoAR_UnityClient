using UnityEngine;

namespace PhotoGalleryService
{
    internal class AndroidCallbackHelper : MonoBehaviour
    {
        private AndroidJob Job { get { return androidPhotoGallery.CurrentJob; } }
        private AndroidPhotoGallery androidPhotoGallery;
        // TODO: AT Create time out if IsFinished takes to long
        private void Update()
        {
            if (Job == null) return;
            if (!Job.IsFinished) return;
            if (Job.CallBackCalled) return;
            Finish();
        }

        private void Finish()
        {
            Job.Finish();
            androidPhotoGallery.Dequeue();
        }

        public void SetPhotoGallery(AndroidPhotoGallery androidPhotoGallery)
        {
            this.androidPhotoGallery = androidPhotoGallery;
        }
    }
}

