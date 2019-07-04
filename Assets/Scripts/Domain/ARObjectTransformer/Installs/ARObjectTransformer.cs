using System;
using Domain.ARObjectSpawnService;
using Infrastructure.Common;
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

        private IARObject selectedARObject;
        private Vector3 selectedARObjectStartPos;
        private IInputSystem inputSystem;

        [Inject]
        private void Construct(IInputSystem inputSystem)
        {
            this.inputSystem = inputSystem;
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
            if (aRObject == null || selectedARObject != null) return;
            selectedARObject = aRObject;
            selectedARObjectStartPos = selectedARObject.Pos;
        }

        public void UnselectedARObject()
        {
            selectedARObject = null;
        }

        public void UpdateObjectPosition(Vector2 delta)
        {
            if (selectedARObject == null) return;
            var cam = Camera.main.transform.position;
            var direction = Utils.GetDirectionBetweenVectors(cam, selectedARObjectStartPos);
            var distance = Vector3.Distance(cam, selectedARObject.Pos);
            var newPos = GetPointAtTouchDistance(Input.touches[0].position, direction, distance);
            selectedARObject.SetPosition(new Vector3(newPos.x, selectedARObject.Pos.y, newPos.z));
        }

        public Vector3 GetPointAtTouchDistance(Vector3 touchPos, Vector3 direction, float distance)
        {
            touchPos.z = distance;
            var worldPos = Camera.main.ScreenToWorldPoint(touchPos);

            return worldPos;
        }
    }
}


