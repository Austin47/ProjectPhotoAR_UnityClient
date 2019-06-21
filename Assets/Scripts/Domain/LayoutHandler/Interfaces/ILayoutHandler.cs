
namespace Domain.LayoutHandlerService
{
    public interface ILayoutHandler
    {
        void ShowLayout(ILayoutEntity layout);
        void GoBack();
        void SetDefaultLayout(ILayoutEntity layout);
    }
}


