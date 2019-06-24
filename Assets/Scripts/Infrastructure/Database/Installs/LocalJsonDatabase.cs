using System;
using System.Collections;
using System.IO;
using Infrastructure.CoroutineRunner;
using PhotoGalleryService;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Infrastructure.DatabaseService
{
    public class LocalJsonDatabase : IDatabase
    {
        private string FileHeader
        {
            get
            {
#if UNITY_EDITOR
                return "file://";
#elif UNITY_ANDROID
            return string.Empty;
#else
            // TODO: AT - might something else for ios
            return string.Empty;
#endif
            }
        }

        private ICoroutineRunner coroutineRunner;
        private IPhotoGallery photoGallery;

        [Inject]
        private void Construct(
            ICoroutineRunner coroutineRunner,
            IPhotoGallery photoGallery)
        {
            this.coroutineRunner = coroutineRunner;
            this.photoGallery = photoGallery;
        }

        public void Load<T>(string url, Action<T> callback)
        {
            coroutineRunner.RunCoroutine(LoadAsync<T>($"{FileHeader}{url}", callback));
        }

        public void LoadDefaultTexture(string url, Action<Texture2D> callback)
        {
            // TODO: Still Noticeable lag spikes while loading
            string path = $"{FileHeader}{Application.streamingAssetsPath}/{url}";
            coroutineRunner.RunCoroutine(GetTexture(path, callback));
        }

        public void LoadTexture(string url, Action<Texture2D> callback)
        {
            string path = $"file://{FileHeader}{url}";
            coroutineRunner.RunCoroutine(GetTexture(path, callback));
        }

        public void LoadCustomTexture(string url, Action<Texture2D> callback)
        {
            // TODO: AT - Use AndroidPhotoGallery
            throw new NotImplementedException();
        }

        public void Save<T>(string url)
        {
            throw new NotImplementedException();
        }

        private IEnumerator LoadAsync<T>(string url, Action<T> callback)
        {
            using (UnityWebRequest uwr = UnityWebRequest.Get(url))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.LogError($"LocalJsonDatabase: LoadAsync: Url: {url} Error: {uwr.error}");
                }
                else
                {
                    var text = uwr.downloadHandler.text;
                    // TODO: what if this is null? is this a problem?
                    var data = JsonUtility.FromJson<T>(text);
                    callback(data);
                }
            }
        }

        private IEnumerator GetTexture(string url, Action<Texture2D> callback)
        {
            Debug.Log($"LocalJsonDatabase: GetTexture: url:{url}");
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.LogError($"LocalJsonDatabase: GetTexture: Url: {url} Error: {uwr.error}");
                }
                else
                {
                    // Get downloaded asset bundle
                    var text = DownloadHandlerTexture.GetContent(uwr);
                    callback(text);
                }
            }
        }

        public void SavePhoto(byte[] image, Action<string> callback)
        {
            photoGallery.SaveToGallery(image, path =>
            {
                // TODO: We need to save this path into a json file
                // for retrieving later
                callback(path);
            });
        }
    }
}

