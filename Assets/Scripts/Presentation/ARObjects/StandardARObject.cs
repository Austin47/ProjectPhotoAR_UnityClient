using System.Collections;
using System.Collections.Generic;
using Domain.ARObjectSpawnService;
using UnityEngine;
using Zenject;

namespace Presentation.ARObjectSpawner
{
    public class StandardARObject : MonoBehaviour, IARObject
    {
        public void SetPos(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}

