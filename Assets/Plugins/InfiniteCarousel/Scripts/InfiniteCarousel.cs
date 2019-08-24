using UnityEngine;
using UnityEngine.EventSystems;

namespace InfiniteCarouselSystem
{
    public class InfiniteCarousel : MonoBehaviour, IDragHandler
    {
        private ICarouselComponent[] components;

        [SerializeField]
        private float xMax = 3000;
        [SerializeField]
        private float xMin = -3000;

        [SerializeField]
        private Transform parent;
        [SerializeField]
        private float gizmosRadius = 50;

        private void Awake()
        {
            components = parent.GetComponentsInChildren<ICarouselComponent>();
            // var width = components[0].GetWidth();
            // xMax = (components.Length / 2) * width + width;
            // xMin = -xMax;
        }

        public void OnDrag(PointerEventData eventData)
        {
            foreach (var c in components)
            {
                c.OnDrag(eventData.delta);

                if (c.GetPosition().x > xMax)
                    MoveToBack(c);
                else if (c.GetPosition().x < xMin)
                    MoveToFront(c);
            }
        }

        private void MoveToFront(ICarouselComponent component)
        {
            component.GetTransform().SetAsLastSibling();
        }

        private void MoveToBack(ICarouselComponent component)
        {
            component.GetTransform().SetAsFirstSibling();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector3(xMin, transform.position.y, transform.position.z), gizmosRadius);
            Gizmos.DrawSphere(new Vector3(xMax, transform.position.y, transform.position.z), gizmosRadius);
        }
    }
}

