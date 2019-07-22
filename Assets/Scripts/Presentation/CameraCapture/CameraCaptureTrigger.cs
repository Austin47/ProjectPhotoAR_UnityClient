using Domain.CameraCaptureService;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Presentation.CameraCaptureService
{
    public class CameraCaptureTrigger : MonoBehaviour
    {
        private float currentCaptureDelay;
        private float captureDelay = 0.3f;

        [SerializeField]
        private UnityEvent onCapture;

        private CameraCapture cameraCapture;

        [Inject]
        private void Construct(CameraCapture cameraCapture) =>
            this.cameraCapture = cameraCapture;

        private void Update()
        {
            if (currentCaptureDelay > 0)
            {
                currentCaptureDelay -= Time.deltaTime;
            }
        }

        public void CaptrueImage()
        {
            if (currentCaptureDelay > 0) return;
            onCapture.Invoke();
            cameraCapture.CapturePhoto();
            currentCaptureDelay = captureDelay;
        }
    }
}


