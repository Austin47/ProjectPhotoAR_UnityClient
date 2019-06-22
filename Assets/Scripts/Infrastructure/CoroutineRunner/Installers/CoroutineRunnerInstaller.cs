using Zenject;

namespace Infrastructure.CoroutineRunner
{
    public class CoroutineRunnerInstaller : MonoInstaller 
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ICoroutineRunner)).To(typeof(CoroutineRunner)).AsSingle();
        }
    }
}


