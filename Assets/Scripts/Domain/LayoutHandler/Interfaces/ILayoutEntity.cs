

namespace Domain.LayoutHandlerService
{
    public interface ILayoutEntity
    {
        void Show();
        void Hide();
        int Order { get; }
    }
}