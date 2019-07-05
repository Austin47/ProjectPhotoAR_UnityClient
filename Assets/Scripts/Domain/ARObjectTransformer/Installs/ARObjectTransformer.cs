using System;
using Domain.ARObjectSpawnService;
using Infrastructure.CameraService;
using Infrastructure.InputService;
using UnityEngine;
using Zenject;

namespace Domain.ARObjectService
{
    public class ARObjectTransformer : IARObjectTransformer, IInitializable, ITickable, IDisposable
    {
        // move ARObject up and down based on access
        // probably going to need to talk to input manager

        // possibly use a TransformTrigger on the ARObject to trigger movability

        // scale object based on input manager

        // rotate object based on input manager

        public IARObject SelectedARObject { get; private set; }

        private IInputSystem inputSystem;
        private ICameraSystem cameraSystem;

        [Inject]
        private void Construct(
            ICameraSystem cameraSystem, 
            IInputSystem inputSystem)
        {
            this.inputSystem = inputSystem;
            this.cameraSystem = cameraSystem;
        }

        public void Initialize()
        {
            inputSystem.OnPanHandler += UpdateObjectPosition;
        }

        public void Dispose()
        {
            inputSystem.OnPanHandler -= UpdateObjectPosition;
        }

        public void Tick()
        {
            if (!inputSystem.IsTouching)
            {
                UnselectedARObject();
                return;
            }
        }

        public void SetSelectedARObject(IARObject aRObject)
        {
            if (aRObject == null || SelectedARObject != null) return;
            SelectedARObject = aRObject;
        }

        public void UnselectedARObject()
        {
            SelectedARObject = null;
        }

        public void UpdateObjectPosition(Vector2 delta)
        {
            if (SelectedARObject == null) return;
            var camPos = cameraSystem.Pos;
            var distance = Vector3.Distance(camPos, SelectedARObject.Pos);
            Vector2 touchPoint;
            inputSystem.GetTouchPos(out touchPoint);
            var newPos = GetPointAtTouchDistance(touchPoint, distance);
            SelectedARObject.SetPosition(new Vector3(newPos.x, SelectedARObject.Pos.y, newPos.z));
        }

        public Vector3 GetPointAtTouchDistance(Vector3 touchPos, float distance)
        {
            touchPos.z = distance;
            var worldPos = Camera.main.ScreenToWorldPoint(touchPos);

            return worldPos;
        }
    }
}


