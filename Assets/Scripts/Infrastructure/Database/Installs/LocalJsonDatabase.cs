using System;
using System.Collections;
using System.IO;
using Infrastructure.CoroutineRunner;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Infrastructure.DatabaseService
{
    public class LocalJsonDatabase : IDatabase
    {
        private ICoroutineRunner coroutineRunner;

        [Inject]
        private void Construct(ICoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;
        }

        public void Load<T>(string url, Action<T> callback)
        {
#if UNITY_EDITOR
            string dataAsJson = File.ReadAllText(url);
            var data = JsonUtility.FromJson<T>(dataAsJson);
            callback(data);
#else
            coroutineRunner.RunCoroutine(LoadAsync<T>(url, callback));
#endif
        }

        public void Save<T>(string url)
        {
            throw new NotImplementedException();
        }

        private IEnumerator LoadAsync<T>(string uri, Action<T> callback)
        {
            using (UnityWebRequest uwr = UnityWebRequest.Get(uri))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.LogError($"LocalJsonDatabase: LoadAsync: {uwr.error}");
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
    }
}

