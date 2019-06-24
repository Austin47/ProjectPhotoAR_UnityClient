using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using Infrastructure.CoroutineRunner;
using Infrastructure.DatabaseService;

namespace Infrastructure.CCSystem
{
    public class CameraCaptureSystem : ICameraCaptureSystem
    {
        private Camera cam { get { return Camera.main; } }

        public static Texture2D m_Texture;

        private RenderTexture renderTexture;
        private Texture2D lastCameraTexture;

        private ARCameraBackground aRCameraBackground;
        private ICoroutineRunner coroutineRunner;
        private IDatabase database;

        public CameraCaptureSystem(
            ARCameraBackground aRCameraBackground,
            RenderTexture renderTexture,
            ICoroutineRunner coroutineRunner,
            IDatabase database)
        {
            this.aRCameraBackground = aRCameraBackground;
            this.renderTexture = renderTexture;
            this.coroutineRunner = coroutineRunner;
            this.database = database;
        }

        public void CapturePhoto(Action<byte[]> callback)
        {
            // ScreenCapture.CaptureScreenshot("Test");

            // database.LoadTexture($"{Application.persistentDataPath}/Test.png", t => {
            //     m_Texture = lastCameraTexture;
            //     callback(t.EncodeToPNG());
            // });

            //Graphics.Blit(null, renderTexture, aRCameraBackground.material);  
            RenderTexture.active = renderTexture;

            // var activeRenderTexture = RenderTexture.active;
            // RenderTexture.active = renderTexture;
            if (lastCameraTexture == null)
                lastCameraTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, true);
            lastCameraTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            lastCameraTexture.Apply();
            RenderTexture.active = null;

            m_Texture = lastCameraTexture;

            callback(lastCameraTexture.EncodeToPNG());
        }
    }
}


