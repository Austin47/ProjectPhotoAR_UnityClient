using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace PhotoGalleryService
{
    public class AndroidPhotoGallery : IPhotoGallery
    {
        internal AndroidJob CurrentJob;
        private Queue<AndroidJob> jobs = new Queue<AndroidJob>();

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
            AddJob(
                execute: () => androidGallery.CallStatic("CallPickPhoto", context, androidCallback),
                callback: () => callback(androidCallback.Result),
                androidCallback: androidCallback);
        }

        public void LoadPhoto(string uri, Action<Texture2D> callback)
        {
            AndroidCallback androidCallback = new AndroidCallback();
            AddJob(
                execute: () => androidGallery.CallStatic("CallLoadPhoto", uri, context, androidCallback),
                callback: () => LoadPhotoAsync(androidCallback.Result, callback),
                androidCallback: androidCallback);
        }

        private void AddJob(Action execute, Action callback, AndroidCallback androidCallback)
        {
            AndroidJob job = new AndroidJob(
                action: execute,
                androidCallback: androidCallback,
                callback: callback);

            jobs.Enqueue(job);
            Dequeue();
        }

        internal void Dequeue()
        {
            if (CurrentJob != null && !CurrentJob.IsFinished) return;
            if (jobs.Count <= 0) return;
            CurrentJob = jobs.Dequeue();
            CurrentJob.Execute();
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

