using Zenject;

namespace Infrastructure.PermissionService
{
    public class PermissionsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            #if UNITY_ANDROID
            Container.Bind(typeof(IPermissions)).To(typeof(AndroidPermissions)).AsSingle();
            #endif
        }
    }
}

