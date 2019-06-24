using Domain.CameraCaptureService;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Presentation.CameraCaptureService
{
    public class CameraCaptureTrigger : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onCapture;

        private CameraCapture cameraCapture;

        [Inject]
        private void Construct(CameraCapture cameraCapture) =>
            this.cameraCapture = cameraCapture;

        public void CaptrueImage()
        {
            onCapture.Invoke();
            cameraCapture.CapturePhoto();
        }
    }
}


