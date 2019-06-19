using System;

namespace PhotoGalleryService
{
    internal class AndroidJob
    {
        public bool IsFinished { get { return androidCallback.IsFinished; } }
        public AndroidCallback androidCallback;
        private Action action;
        private Action callback;

        public AndroidJob(Action action, AndroidCallback androidCallback, Action callback)
        {
            this.androidCallback = androidCallback;
            this.action = action;
            this.callback = callback;
        }

        public void Execute()
        {
            action();
        }

        public void Finish()
        {
            callback();
        }
    }
}

