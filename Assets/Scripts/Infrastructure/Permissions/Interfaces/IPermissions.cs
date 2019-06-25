
namespace Infrastructure.PermissionService
{
    public interface IPermissions
    {
        void CheckCamera();
        void CheckReadStorage();
        void CheckWriteStorage();
    }
}

