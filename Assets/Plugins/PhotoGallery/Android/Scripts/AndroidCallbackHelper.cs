using UnityEngine;

namespace PhotoGalleryService
{
    internal class AndroidCallbackHelper : MonoBehaviour
    {
        private AndroidJob ForegroundJob { get { return androidPhotoGallery.CurrentForegroundJob; } }
        private AndroidJob BackgroundJob { get { return androidPhotoGallery.CurrentBackgroundJob; } }
        private AndroidPhotoGallery androidPhotoGallery;
        // TODO: AT Create time out if IsFinished takes to long
        private void Update()
        {
            CheckForegroundJob();
            CheckBackgroundJob();
        }

        private void CheckForegroundJob()
        {
            if (ForegroundJob == null) return;
            if (!ForegroundJob.IsFinished) return;
            if (ForegroundJob.CallbackCalled) return;
            ForegroundJob.Finish();
        }

        private void CheckBackgroundJob()
        {
            if (BackgroundJob == null) return;
            if (!BackgroundJob.IsFinished) return;
            if (BackgroundJob.CallbackCalled) return;
            BackgroundJobFinish();
        }

        private void BackgroundJobFinish()
        {
            BackgroundJob.Finish();
            androidPhotoGallery.Dequeue();
        }

        public void SetPhotoGallery(AndroidPhotoGallery androidPhotoGallery)
        {
            this.androidPhotoGallery = androidPhotoGallery;
        }
    }
}

