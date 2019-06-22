using Zenject;

namespace Domain.ARObjectDatabaseService
{
    public class ARObjectDatabaseInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IARObjectDatabase), typeof(IInitializable)).To(typeof(ARObjectDatabase)).AsSingle();
        }
    }
}


