using System;

namespace Infrastructure.CCSystem
{
    public interface ICameraCaptureSystem
    {
        void CapturePhoto(Action<string> callback);
    }
}