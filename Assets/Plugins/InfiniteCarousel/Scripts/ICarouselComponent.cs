using UnityEngine;

namespace InfiniteCarouselSystem
{
    public interface ICarouselComponent
    {
        void OnDrag(Vector2 delta);
    }
}

