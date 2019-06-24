using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace PhotoGalleryService
{
    public class AndroidPhotoGallery : IPhotoGallery
    {
        internal AndroidJob CurrentBackgroundJob { get; private set; }
        internal AndroidJob CurrentForegroundJob { get; private set; }
        private Queue<AndroidJob> backgroundJobs = new Queue<AndroidJob>();

        private AndroidCallbackHelper androidCallbackHelper;
        private AndroidJavaObject androidGallery;
        private AndroidJavaObject context;

        public AndroidPhotoGallery()
        {
            androidGallery = new AndroidJavaObject("com.absurd.photogallery.AndroidGalleryJ");
            using (AndroidJavaObject unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                context = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }

            GameObject helper = new GameObject("AndroidCallbackHelper");
            androidCallbackHelper = helper.AddComponent<AndroidCallbackHelper>();
            androidCallbackHelper.SetPhotoGallery(this);
        }

        public void SelectPhotoPath(Action<string> callback)
        {
            AndroidCallback androidCallback = new AndroidCallback();
            AddForegroundJob(
                execute: () => androidGallery.CallStatic("CallPickPhoto", context, androidCallback),
                callback: () =>
                {
                    Debug.Log($"AndroidGallery finished SelectPhotoPath call with result {androidCallback.Result}");
                    callback(androidCallback.Result);
                },
                androidCallback: androidCallback);
        }

        private void AddForegroundJob(Action execute, Action callback, AndroidCallback androidCallback)
        {
            if (CurrentBackgroundJob != null && !CurrentForegroundJob.CallbackCalled)
            {
                Debug.LogError("AndroidPhotoGallery: There can only be one foreground task at a time");
                return;
            };
            AndroidJob job = new AndroidJob(
                action: execute,
                androidCallback: androidCallback,
                callback: callback);

            CurrentForegroundJob = job;
            job.Execute();
        }

        // TODO: AT - this is actually save photo in app the returns the image
        // need to refactor name and hard coded path
        // we can load the photo using the Database
        public void LoadPhoto(string uri, Action<Texture2D> callback)
        {
            AndroidCallback androidCallback = new AndroidCallback();
            AddBackgroundJob(
                execute: () => androidGallery.CallStatic("CallLoadPhoto", uri, context, androidCallback),
                callback: () =>
                {
                    Debug.Log($"AndroidGallery finished LoadPhoto call with result {androidCallback.Result}");
                    androidCallbackHelper.StartCoroutine(GetTexture(androidCallback.Result, callback));
                },
                androidCallback: androidCallback);
        }

        public void SaveToGallery(byte[] image, Action<string> callback)
        {
            AndroidCallback androidCallback = new AndroidCallback();
            AddBackgroundJob(
                 execute: () => androidGallery.CallStatic("SaveToGallery", image, context, androidCallback),
                callback: () =>
                {
                    Debug.Log($"AndroidGallery finished SaveToGallery call with result {androidCallback.Result}");
                    callback(androidCallback.Result);
                },
                androidCallback: androidCallback);
        }

        private void AddBackgroundJob(Action execute, Action callback, AndroidCallback androidCallback)
        {
            AndroidJob job = new AndroidJob(
                action: execute,
                androidCallback: androidCallback,
                callback: callback);

            backgroundJobs.Enqueue(job);
            Dequeue();
        }

        internal void Dequeue()
        {
            if (CurrentBackgroundJob != null && !CurrentBackgroundJob.CallbackCalled) return;
            if (backgroundJobs.Count <= 0) return;
            CurrentBackgroundJob = backgroundJobs.Dequeue();
            CurrentBackgroundJob.Execute();
        }

        private IEnumerator GetTexture(string path, Action<Texture2D> callback)
        {
            var url = $"file://{path}";
            Debug.Log($"AndroidGallery: GetText: : Application.persistentDataPath = {Application.persistentDataPath}");
            Debug.Log($"AndroidGallery: GetText: path = {path}");
            Debug.Log($"AndroidGallery: GetText: Attempting to get texture at: {url}");
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.LogError($"AndroidGallery: GetText: {uwr.error}");
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

