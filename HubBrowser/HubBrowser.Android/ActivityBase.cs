using System;using Android.App;
using Caliburn.Micro;

namespace HubBrowser.Android
{
    public class ActivityBase<T> : Activity
    {
        public ActivityBase()
        {
            ViewModel = IoC.Get<T>();

            var viewAware = ViewModel as IViewAware;

            if (viewAware != null)
            {
                viewAware.AttachView(this);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            var activate = ViewModel as IActivate;

            if (activate != null)
            {
                activate.Activate();
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            var deactivate = ViewModel as IDeactivate;

            if (deactivate != null)
            {
                deactivate.Deactivate(false);
            }
        }

        protected T ViewModel { get; private set; }
    }
}