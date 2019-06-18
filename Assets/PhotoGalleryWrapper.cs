using UnityEngine;

public class PhotoGalleryWrapper : MonoBehaviour
{
    private static AndroidJavaObject context;
    private static AndroidJavaObject Context
    {
        get
        {
            if (context == null)
            {
                using (AndroidJavaObject unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    context = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
                }
            }

            return context;
        }
    }

    private static AndroidJavaObject androidGallery;
    private static AndroidJavaObject AndroidGallery
    {
        get
        {
            if (androidGallery == null)
            {
                androidGallery = new AndroidJavaObject("com.absurd.photogallery.AndroidGalleryJ");
            }

            return androidGallery;
        }
    }

    public void PickPhoto()
    {
        AndroidGallery.CallStatic("Call", Context);
    }
}

