using Domain.CameraCaptureService;
using Infrastructure.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Presentation.CameraCaptureService
{
    public class CameraCaptureVisual : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onCapture;
        [SerializeField]
        private RawImage image;

        private CameraCapture cameraCapture;
        private float defaultImageSize;

        [Inject]
        private void Construct(CameraCapture cameraCapture) =>
            this.cameraCapture = cameraCapture;

        private void Start()
        {
            cameraCapture.OnCapturePhoto += OnCapturePhoto;
            defaultImageSize = image.rectTransform.rect.width;
        }

        private void OnDestroy()
        {
            cameraCapture.OnCapturePhoto -= OnCapturePhoto;
        }

        private void OnCapturePhoto(Texture2D texture)
        {
            image.texture = texture;
            if(!image.enabled) image.enabled = true;
            Utils.EnvelopeToValueFromTexture2D(image, texture, defaultImageSize);
            onCapture.Invoke();
        }
    }
}

