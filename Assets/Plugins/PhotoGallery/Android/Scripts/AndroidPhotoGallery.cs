using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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
                callback: () => callback(androidCallback.Result),
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

        public void LoadPhoto(string uri, Action<Texture2D> callback)
        {
            AndroidCallback androidCallback = new AndroidCallback();
            AddBackgroundJob(
                execute: () => androidGallery.CallStatic("CallLoadPhoto", uri, context, androidCallback),
                callback: () => LoadPhotoAsync(androidCallback.Result, callback),
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

        private void LoadPhotoAsync(string imageBase64, Action<Texture2D> callback)
        {
            Task.Factory.StartNew(() =>
            {
                return LoadBase64Bytes(imageBase64);
            }).Unwrap().ContinueWith(task =>
            {
                Texture2D text = new Texture2D(1, 1);
                // TODO: AT - This action still causes a spike, need to find a way to optimize 
                text.LoadImage(task.Result);
                callback(text);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task<byte[]> LoadBase64Bytes(string imageBase64)
        {
            byte[] b = new byte[] { };
            await Task.Run(() => b = Convert.FromBase64String(imageBase64));
            return b;
        }
    }
}

