using System.Collections;
using System.Collections.Generic;
using Domain.ARObjectSpawnService;
using UnityEngine;
using Zenject;

namespace Presentation.ARObjectSpawner
{
    public class StandardARObject : MonoBehaviour, IARObject
    {
        public void Configure(Vector3 pos, Texture2D text)
        {
            transform.position = pos;
        }
    }
}

