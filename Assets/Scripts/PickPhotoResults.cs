using System;

public class PickPhotoResults : UnityEngine.AndroidJavaProxy
{
    public PickPhotoResults() : base("com.absurd.photogallery.PickPhotoResults") { }

    Action<string> callBack;

    public void SetCallback(Action<string> cb)
    {
        callBack = cb;
    }

    public void OnFinished(String imageBase64)
    {
        callBack(imageBase64);
    }
}

