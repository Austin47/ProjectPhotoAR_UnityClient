using Domain.CameraCaptureService;
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

        [Inject]
        private void Construct(CameraCapture cameraCapture) =>
            this.cameraCapture = cameraCapture;

        private void Start()
        {
            cameraCapture.OnCapturePhoto += OnCapturePhoto;
        }

        private void OnDestroy()
        {
            cameraCapture.OnCapturePhoto -= OnCapturePhoto;
        }

        private void OnCapturePhoto(Texture2D obj)
        {
            image.texture = obj;
            if(!image.enabled) image.enabled = true;
            onCapture.Invoke();
        }
    }
}

