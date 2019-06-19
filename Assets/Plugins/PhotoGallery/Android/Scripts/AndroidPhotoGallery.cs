using System;
using System.Collections.Generic;
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
                callback: () => callback(LoadPhoto(androidCallback.Result)),
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

        // TODO: AT - Make async task
        private Texture2D LoadPhoto(string imageBase64)
        {
            Texture2D text = new Texture2D(1, 1);
            text.LoadImage(Convert.FromBase64String(imageBase64));
            return text;
        }
    }
}

