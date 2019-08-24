using UnityEngine;

namespace InfiniteCarouselSystem
{
    public interface ICarouselComponent
    {
        float GetWidth();
        Vector3 GetPosition();
        Transform GetTransform();
        void OnDrag(Vector2 delta);
    }
}

