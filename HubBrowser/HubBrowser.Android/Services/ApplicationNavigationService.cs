using System;
using Android.App;
using Android.Content;
using Caliburn.Micro;
using HubBrowser.Core.Services;

namespace HubBrowser.Android.Services
{
    public class ApplicationNavigationService : IApplicationNavigationService
    {
        private readonly Application application;
        private readonly ActivityLifecycleCallbackHandler lifecycleHandler = new ActivityLifecycleCallbackHandler();
        private Activity currentActivity;

        public ApplicationNavigationService(Application application)
        {
            this.application = application;
            this.application.RegisterActivityLifecycleCallbacks(lifecycleHandler);

            lifecycleHandler.ActivityResumed += OnActivityResumed;
            lifecycleHandler.ActivityPaused += OnActivityPaused;
        }

        private void OnActivityResumed(object sender, ActivityEventArgs e)
        {
            currentActivity = e.Activity;
        }

        private void OnActivityPaused(object sender, ActivityEventArgs e)
        {
            if (currentActivity == e.Activity)
                currentActivity = null;
        }

        private void NavigateToActivity<T>()
        {
            var intent = new Intent(currentActivity, typeof(T));

            currentActivity.StartActivity(intent);
        }

        public void ToRepositoryList()
        {
            NavigateToActivity<RepositoryListActivity>();
        }
    }
}