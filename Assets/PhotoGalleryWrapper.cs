using System.Collections;
using PhotoGalleryService;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PhotoGalleryWrapper : MonoBehaviour
{
    public RawImage image;
    public IPhotoGallery photoGallery;

    private void Start()
    {
        photoGallery = new AndroidPhotoGallery();
    }

    public void UpdateImage()
    {
        photoGallery.SelectPhotoPath(s =>
        {
            // Stress test
            photoGallery.LoadPhoto(s, text =>
            {
                image.texture = text;
            });
        });
    }

    IEnumerator GetText(string uri)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(uri))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                var text = DownloadHandlerTexture.GetContent(uwr);
                image.texture = text;
            }

        }
    }
}

