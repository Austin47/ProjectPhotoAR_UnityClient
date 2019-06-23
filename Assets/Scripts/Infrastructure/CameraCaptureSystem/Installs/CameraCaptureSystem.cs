
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System;

namespace Infrastructure.CCSystem
{
    public class CameraCaptureSystem
    {
        private readonly ARCameraManager CameraManager;

        public CameraCaptureSystem(ARCameraManager cameraManager) => CameraManager = cameraManager;

        public void CapturePhoto(Action<byte[]> callback)
        {
            GetImageAsync(callback);
        } 

        // Ref: https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@1.0/manual/cpu-camera-image.html
        private void GetImageAsync(Action<byte[]> callback)
        {
            // Get information about the camera image
            XRCameraImage image;
            if (!CameraManager.TryGetLatestImage(out image))
            {
                return;
            }
            // If successful, launch a coroutine that waits for the image
            // to be ready, then apply it to a texture.
            image.ConvertAsync(new XRCameraImageConversionParams
            {
                // Get the full image
                inputRect = new RectInt(0, 0, image.width, image.height),

                // Downsample by 2
                outputDimensions = new Vector2Int(image.width / 2, image.height / 2),

                // Color image format
                outputFormat = TextureFormat.RGB24,

                // Flip across the Y axis
                transformation = CameraImageTransformation.MirrorY

                // Call ProcessImage when the async operation completes
            }, (status, conversionParams, data) =>
            {
                ProcessImage(status, conversionParams, data, callback);
            });

            // It is safe to dispose the image before the async operation completes.
            image.Dispose();
        }

        private void ProcessImage(
            AsyncCameraImageConversionStatus status, 
            XRCameraImageConversionParams conversionParams, 
            NativeArray<byte> data, 
            Action<byte[]> callback)
        {
            if (status != AsyncCameraImageConversionStatus.Ready)
            {
                Debug.LogErrorFormat("Async request failed with status {0}", status);
                return;
            }

            callback(data.ToArray());
        }
    }
}


