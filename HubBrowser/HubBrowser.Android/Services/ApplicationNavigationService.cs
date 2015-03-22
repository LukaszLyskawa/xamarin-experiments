using System;
using Android.Content;
using HubBrowser.Core.Services;

namespace HubBrowser.Android.Services
{
    public class ApplicationNavigationService : INavigationService
    {
        private readonly Application application;

        public ApplicationNavigationService(Application application)
        {
            this.application = application;
        }

        private void NavigateToActivity<T>()
        {
            var intent = new Intent(application, typeof (T));

            application.StartActivity(intent);
        }

        public void ToRepositoryList()
        {
            NavigateToActivity<RepositoryListActivity>();
        }
    }
}