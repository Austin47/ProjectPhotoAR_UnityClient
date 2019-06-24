using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using Infrastructure.CoroutineRunner;
using Infrastructure.DatabaseService;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace Infrastructure.CCSystem
{
    internal class CameraSystemHelper : MonoBehaviour
    {
        private Queue<Action> requests = new Queue<Action>();
        public void OnPostRender()
        {
            if (requests.Count <= 0) return;

            requests.Dequeue()();
        }

        public void AddRequest(Action request)
        {
            requests.Enqueue(request);
        }
    }

    public class CameraCaptureSystem : ICameraCaptureSystem
    {
        private Camera cam { get { return Camera.main; } }
        private CameraSystemHelper helper;

        public static Texture2D m_Texture;

        private RenderTexture renderTexture;
        private Texture2D lastCameraTexture;

        private Canvas canvas;
        private ARCameraBackground aRCameraBackground;
        private ICoroutineRunner coroutineRunner;
        private IDatabase database;

        public CameraCaptureSystem(
            Canvas canvas,
            ARCameraBackground aRCameraBackground,
            RenderTexture renderTexture,
            ICoroutineRunner coroutineRunner,
            IDatabase database)
        {
            this.canvas = canvas;
            this.aRCameraBackground = aRCameraBackground;
            this.renderTexture = renderTexture;
            this.coroutineRunner = coroutineRunner;
            this.database = database;

            helper = cam.gameObject.AddComponent<CameraSystemHelper>();
        }

        public void CapturePhoto(Action<byte[]> callback)
        {
            coroutineRunner.RunCoroutine(CapturePhotoAsync(callback));
        }

        private IEnumerator CapturePhotoAsync(Action<byte[]> callback)
        {
            canvas.enabled = false;
            yield return new WaitForEndOfFrame();
            ScreenCapture.CaptureScreenshot("Test.png");
            canvas.enabled = true;
            // TODO: AT - This wont work if it takes longer than 0.5 seconds to take the picture
            yield return new WaitForSeconds(0.5f);
            database.LoadTexture($"{Application.persistentDataPath}/Test.png", t =>
            {
                m_Texture = t;
                callback(new byte[] { });
            });
        }
    }
}


