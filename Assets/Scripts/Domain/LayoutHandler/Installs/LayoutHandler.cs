using System.Collections.Generic;
using UnityEngine;

namespace Domain.LayoutHandlerService
{
    public class LayoutHandler : ILayoutHandler
    {
        private Stack<ILayoutEntity> layouts = new Stack<ILayoutEntity>();
        public ILayoutEntity DefaultLayout { get; private set; }

        public ILayoutEntity TopLayout { get 
        {
            if (layouts.Count <= 0) return DefaultLayout;
            return layouts.Peek();
        }}

        public void SetDefaultLayout(ILayoutEntity layout)
        {
            if (DefaultLayout != null)
            {
                Debug.LogError("LayoutHandler: SetDefaultLayout: defaultLayout has already been set!");
                return;
            }

            DefaultLayout = layout;
        }

        public void ShowLayout(ILayoutEntity layout)
        {
            if (layouts.Count <= 0)
            {
                AddLayout(layout);
                return;
            }

            var count = layouts.Count;
            for (int i = 0; i < count; i++)
            {
                if (layout.Order > layouts.Peek().Order)
                {
                    AddLayout(layout);
                    return;
                }

                GoBack();
            }
        }

        private void AddLayout(ILayoutEntity layout)
        {
            layout.Show();
            if (layout == DefaultLayout) return;
            layouts.Push(layout);
        }

        public void GoBack()
        {
            if (layouts.Count <= 0) return;

            var topLayout = layouts.Pop();
            topLayout.Hide();
        }
    }
}


