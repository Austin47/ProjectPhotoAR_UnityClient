using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Presentation.ARObjectUI
{
    public class ARObjectUIBlock : MonoBehaviour, IARObjectUIBlock
    {
        public void Configure(Transform root)
        {
            transform.SetParent(root);
            transform.localScale = Vector3.one;
        }
    }
}

