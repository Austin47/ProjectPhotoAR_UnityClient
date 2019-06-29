using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Zenject;

namespace Infrastructure.RaycastService
{
    public class RaycastSystem : IRaycastSystem, IInitializable
    {
        private List<ARRaycastHit> getPlanePointList = new List<ARRaycastHit>();

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
            if (!aRRaycastManager.Raycast(pos, list, TrackableType.All)) return false;
            spawnPoint = list[0].pose.position;
            return true;
        }

        public bool TryToGetPlanePoint(Vector3 start, Vector3 direction, out Vector3 planePoint)
        {
            planePoint = new Vector3();
            Ray ray = new Ray(start, direction);
            getPlanePointList = new List<ARRaycastHit>();
            if (!aRRaycastManager.Raycast(ray, getPlanePointList, TrackableType.Planes)) return false;
            planePoint = getPlanePointList[0].pose.position;
            return true;
        }
    }
}

