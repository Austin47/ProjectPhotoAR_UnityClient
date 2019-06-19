using UnityEngine;

namespace PhotoGalleryService
{
    internal class AndroidCallbackHelper : MonoBehaviour
    {
        private AndroidJob job;
        private AndroidPhotoGallery androidPhotoGallery;
        // TODO: AT Create time out if IsFinished takes to long
        private void Update()
        {
            if (job == null) return;
            if (!job.IsFinished) return;
            Finish();
        }

        private void Finish()
        {
            job.Finish();
            job = null;
            androidPhotoGallery.Dequeue();
        }

        public void SetPhotoGallery(AndroidPhotoGallery androidPhotoGallery)
        {
            this.androidPhotoGallery = androidPhotoGallery;
        }

        public void WaitForJobFinish(AndroidJob job)
        {
            this.job = job;
        }
    }
}

