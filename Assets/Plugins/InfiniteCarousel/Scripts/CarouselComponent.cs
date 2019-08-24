using UnityEngine;

namespace InfiniteCarouselSystem
{
    public class CarouselComponent : MonoBehaviour, ICarouselComponent
    {
        [SerializeField]
        private RectTransform rect;

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public float GetWidth()
        {
            return rect.sizeDelta.x;
        }

        public void OnDrag(Vector2 delta)
        {
            transform.position = new Vector3(
                transform.position.x + delta.x,
                transform.position.y,
                transform.position.z);
        }
    }
}

