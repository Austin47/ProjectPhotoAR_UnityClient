using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InfiniteCarouselSystem
{
    public class InfiniteCarousel : MonoBehaviour, IDragHandler
    {
        private ICarouselComponent[] components;

        [SerializeField]
        private Transform parent;

        private void Awake()
        {
            components = parent.GetComponentsInChildren<ICarouselComponent>();
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            foreach (var c in components)
            {
                c.OnDrag(eventData.delta);
            }
        }
    }
}

