using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Zenject;

namespace Infrastructure.RaycastService
{
    public class RaycastSystem : IRaycastSystem, IInitializable
    {
        private ARRaycastManager aRRaycastManager;

        public void Initialize()
        {
            GameObject obj = new GameObject("ARRaycastManager");
            aRRaycastManager = obj.AddComponent<ARRaycastManager>();
            UnityEngine.Object.DontDestroyOnLoad(obj);
        }

        public bool TryTouchPosToARPlane(Vector2 pos, out Vector3 spawnPoint)
        {
            spawnPoint = new Vector3();
            List<ARRaycastHit> list = new List<ARRaycastHit>();
            aRRaycastManager.Raycast(pos, list, TrackableType.All);
            if (list.Count <= 0) return false;
            spawnPoint = list[0].pose.position;
            return true;
        }
    }
}

