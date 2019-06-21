using Zenject;

namespace Domain.LayoutHandlerService
{
    public class LayoutHandlerInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ILayoutHandler)).To(typeof(LayoutHandler)).AsSingle();
        }
    }
}


