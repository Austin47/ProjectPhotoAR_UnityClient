using UnityEngine;
using System;
using Infrastructure.CoroutineRunner;
using Infrastructure.DatabaseService;
using System.Collections;

namespace Infrastructure.CCSystem
{
    public class CameraCaptureSystem : ICameraCaptureSystem
    {
        private int jobCount;

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
            jobCount++;
            canvas.enabled = false;
            var fileName = $"temp-{jobCount}.png";
            database.DeleteTextureFromLocalApp(fileName);
            yield return new WaitForEndOfFrame();
            ScreenCapture.CaptureScreenshot(fileName);
            canvas.enabled = true;
            yield return new WaitUntil(() => database.TextureFromLocalAppExist(fileName));
            jobCount--;
            callback(fileName);
        }
    }
}


