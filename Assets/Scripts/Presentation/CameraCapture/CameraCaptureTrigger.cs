using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Presentation.CameraCaptureService
{
    public class CameraCaptureTrigger : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onCapture;

        public void CaptrueImage()
        {
            onCapture.Invoke();
        }
    }
}


