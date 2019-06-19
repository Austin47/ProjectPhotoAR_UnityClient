using System;
using PhotoGalleryService;
using UnityEngine;
using UnityEngine.UI;

public class PhotoGalleryWrapper : MonoBehaviour
{
    public Image image;
    public IPhotoGallery photoGallery;

    private void Start()
    {
        photoGallery = new AndroidPhotoGallery();
    }

    public void UpdateImage()
    {
        photoGallery.SelectPhotoPath(s =>
        {
            photoGallery.LoadPhoto(s, text =>
            {
                Sprite sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(.5f, .5f));
                image.sprite = sprite;
            });
        });
        // byte[] decodedBytes = Convert.FromBase64String(imageBase64);

        // Texture2D text = new Texture2D(1, 1);
        // text.LoadImage(Convert.FromBase64String(imageBase64));

        // Sprite sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(.5f, .5f));
        // image.sprite = sprite;
        // imageBase64 = null;
    }
}

