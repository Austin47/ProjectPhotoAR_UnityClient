using System;
using UnityEngine;
using UnityEngine.UI;

public class PhotoGalleryWrapper : MonoBehaviour
{
    public Image image;

    private string imageBase64;

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

    private void Update()
    {
        while (string.IsNullOrWhiteSpace(imageBase64)) return;
        UpdateImage();
    }

    public void PickPhoto()
    {
        PickPhotoResults pickPhotoResults = new PickPhotoResults();
        pickPhotoResults.SetCallback(Callback);
        AndroidGallery.CallStatic("CallPickPhoto", Context, pickPhotoResults);
    }

    private void Callback(string imageBase64)
    {
        this.imageBase64 = imageBase64;
    }

    private void UpdateImage()
    {
        byte[] decodedBytes = Convert.FromBase64String(imageBase64);

        Texture2D text = new Texture2D(1, 1);
        text.LoadImage(Convert.FromBase64String(imageBase64));

        Sprite sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(.5f, .5f));
        image.sprite = sprite;
        imageBase64 = null;
    }
}

