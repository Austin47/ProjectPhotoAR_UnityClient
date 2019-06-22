using Zenject;

namespace Infrastructure.DatabaseService
{
    public class DatabaseInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IDatabase)).To(typeof(LocalJsonDatabase)).AsSingle();
        }
    }
}


