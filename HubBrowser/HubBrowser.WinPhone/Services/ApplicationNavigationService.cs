using System;
using Caliburn.Micro;
using HubBrowser.Core.Services;
using HubBrowser.Core.ViewModels;

namespace HubBrowser.WinPhone.Services
{
    public class ApplicationNavigationService : IApplicationNavigationService
    {
        private readonly INavigationService navigationService;

        public ApplicationNavigationService(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public void ToRepositoryList()
        {
            navigationService
                .UriFor<RepositoryListViewModel>()
                .Navigate();
        }
    }
}
