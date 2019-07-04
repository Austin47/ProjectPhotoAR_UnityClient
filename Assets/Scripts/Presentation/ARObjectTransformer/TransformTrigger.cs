using Domain.ARObjectService;
using Domain.ARObjectSpawnService;
using UnityEngine;
using Zenject;

namespace Presentation.ARObjectService
{
    public class TransformTrigger : MonoBehaviour
    {
        [SerializeField]
        private GameObject aRObjectParent;

        private IARObject aRObject;

        private IARObjectTransformer aRObjectTransformer;

        [Inject]
        private void Construct(IARObjectTransformer aRObjectTransformer)
        {
            this.aRObjectTransformer = aRObjectTransformer;
        }

        private void Awake()
        {
            aRObject =  aRObjectParent.GetComponent<IARObject>();
            if(aRObject == null) Debug.LogError("TransformTrigger: Awake: aRObject == null");
        }

        private void OnMouseDown()
        {
            aRObjectTransformer.SetSelectedARObject(aRObject);
        }
    }
}

