using Domain.LayoutHandlerService;
using TweenService;
using UnityEngine;
using Zenject;

namespace Presentation.LayoutHandlerService
{
    public class LayoutEntity : MonoBehaviour, ILayoutEntity
    {
        [SerializeField]
        private bool isDefaultLayout;
        [SerializeField]
        private TweenSequencer tweenSequencer;
        [Range(0, 15)]
        [SerializeField]
        private int order;
        public int Order { get { return order; } }

        private ILayoutHandler layoutHandler;

        [Inject]
        private void Construct(ILayoutHandler layoutHandler)
        {
            this.layoutHandler = layoutHandler;
        }
        
        private void Start()
        {
            if (!isDefaultLayout) return;
            layoutHandler.SetDefaultLayout(this);
        }

        public void ShowWithLayoutHandler()
        {
            layoutHandler.ShowLayout(this);
        }

        public void Show()
        {
            tweenSequencer.ToggleOn();
        }

        public void Hide()
        {
            tweenSequencer.ToggleOff();
        }
    }
}

