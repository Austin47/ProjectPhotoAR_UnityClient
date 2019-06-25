using System;
using System.Collections;
using Infrastructure.Common;
using Infrastructure.CoroutineRunner;
using Infrastructure.PermissionService;
using PhotoGalleryService;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Infrastructure.DatabaseService
{
    public class LocalJsonDatabase : IDatabase
    {
        private ICoroutineRunner coroutineRunner;
        private IPhotoGallery photoGallery;
        private IPermissions permissions;

        [Inject]
        private void Construct(
            ICoroutineRunner coroutineRunner,
            IPhotoGallery photoGallery,
            IPermissions permissions)
        {
            this.coroutineRunner = coroutineRunner;
            this.photoGallery = photoGallery;
            this.permissions = permissions;
        }

        public void LoadFromStreamAssets<T>(string url, Action<T> callback)
        {
            coroutineRunner.RunCoroutine(LoadAsync<T>($"{FilePath.StreamingAssets}/{url}", callback));
        }

        public void LoadTextureFromStringmAssets(string url, Action<Texture2D> callback)
        {
            string path = $"{FilePath.StreamingAssets}/{url}";
            coroutineRunner.RunCoroutine(GetTexture(path, callback));
        }

        public void LoadTextureFromLocalApp(string url, Action<Texture2D> callback)
        {
            string path = $"{FilePath.Local}/{url}";
            coroutineRunner.RunCoroutine(GetTexture(path, callback));
        }

        public void LoadTextureFromGallery(string url, Action<Texture2D> callback)
        {
            // TODO: Android gallery?
            throw new NotImplementedException();
            // string path = $"{FilePath.Gallery}/{url}";
            // coroutineRunner.RunCoroutine(GetTexture(path, callback));
        }

        public void SavePathToLocal(string url)
        {
            // TODO: Save url to json file, so we can load pictures later
            //throw new NotImplementedException();
        }

        private IEnumerator LoadAsync<T>(string url, Action<T> callback)
        {
            permissions.CheckReadStorage();
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
            permissions.CheckReadStorage();
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
    }
}

