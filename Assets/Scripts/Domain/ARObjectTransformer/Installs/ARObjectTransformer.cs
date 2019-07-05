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
        // TODO: AT - rotate object based on input manager

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
            inputSystem.OnPinchHandler += UpdateObjectScale;
        }

        public void Dispose()
        {
            inputSystem.OnPanHandler -= UpdateObjectPosition;
            inputSystem.OnPinchHandler -= UpdateObjectScale;
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

        public void UpdateObjectScale(float delta)
        {
            if (SelectedARObject == null) return;
            SelectedARObject.SetScale(SelectedARObject.Scale * (delta + 1));
        }

        public Vector3 GetPointAtTouchDistance(Vector3 touchPos, float distance)
        {
            touchPos.z = distance;
            var worldPos = cameraSystem.ScreenToWorldPoint(touchPos);

            return worldPos;
        }
    }
}


