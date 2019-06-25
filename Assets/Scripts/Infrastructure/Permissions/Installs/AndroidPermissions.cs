#if UNITY_ANDROID
using UnityEngine.Android;
#endif

namespace Infrastructure.PermissionService
{
    public class AndroidPermissions : IPermissions
    {
        public void CheckCamera()
        {
#if UNITY_ANDROID
            if (Permission.HasUserAuthorizedPermission(Permission.Camera)) return;
            Permission.RequestUserPermission(Permission.Camera);
#endif
        }

        public void CheckReadStorage()
        {
#if UNITY_ANDROID
            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)) return;
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
#endif
        }

        public void CheckWriteStorage()
        {
#if UNITY_ANDROID
            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)) return;
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
#endif
        }
    }
}

