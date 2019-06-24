using System;
using Domain.CameraCaptureService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Presentation.CameraCaptureService
{
    public class CameraCaptureTrigger : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onCapture;
        [SerializeField]
        private RawImage texture;

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
            texture.texture = Infrastructure.CCSystem.CameraCaptureSystem.m_Texture;
        }

        public void CaptrueImage()
        {
            onCapture.Invoke();
            cameraCapture.CapturePhoto();
        }
    }
}


