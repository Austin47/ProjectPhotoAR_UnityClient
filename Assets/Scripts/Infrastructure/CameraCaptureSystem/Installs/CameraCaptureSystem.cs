using UnityEngine;
using System;
using Infrastructure.CoroutineRunner;
using Infrastructure.DatabaseService;
using System.Collections;

namespace Infrastructure.CCSystem
{
    public class CameraCaptureSystem : ICameraCaptureSystem
    {
        private Canvas canvas;
        private ICoroutineRunner coroutineRunner;
        private IDatabase database;

        public CameraCaptureSystem(
            Canvas canvas,
            ICoroutineRunner coroutineRunner,
            IDatabase database)
        {
            this.canvas = canvas;
            this.coroutineRunner = coroutineRunner;
            this.database = database;
        }

        public void CapturePhoto(Action<string> callback)
        {
            coroutineRunner.RunCoroutine(CapturePhotoAsync(callback));
        }

        private IEnumerator CapturePhotoAsync(Action<string> callback)
        {
            // HACK: To make ui not show up in ScreenCapture we temporarily hide the canvas
            // TODO: AT - Not very clean, need to replace
            canvas.enabled = false;
            yield return new WaitForEndOfFrame();
            var fileName = "temp.png";
            ScreenCapture.CaptureScreenshot(fileName);
            canvas.enabled = true;
            // TODO: AT - This wont work if it takes longer than 0.6 seconds to take the picture
            // need a way to precisely tell when the photo is ready 
            yield return new WaitForSeconds(0.6f);
            callback(fileName);
        }
    }
}


