using PhotoGalleryService;
using Zenject;

namespace Infrastructure.PhotoGalleryService
{
    public class PhotoGalleryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IPhotoGallery)).To(typeof(AndroidPhotoGallery)).AsSingle();
        }
    }
}

