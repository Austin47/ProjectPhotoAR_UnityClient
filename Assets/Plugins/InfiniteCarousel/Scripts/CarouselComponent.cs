using UnityEngine;

namespace InfiniteCarouselSystem
{
    public class CarouselComponent : MonoBehaviour, ICarouselComponent
    {
        public void OnDrag(Vector2 delta)
        {
            transform.position = new Vector3(
                transform.position.x + delta.x,
                transform.position.y,
                transform.position.z);
        }
    }
}

