using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using HubBrowser.Core.Services;
using Octokit;

namespace HubBrowser.Core.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IGitHubClient gitHubClient;
        private readonly INavigationService navigationService;

        public LoginViewModel(IGitHubClient gitHubClient, INavigationService navigationService)
        {
            this.gitHubClient = gitHubClient;
            this.navigationService = navigationService;
        }

        public bool CanSignIn(string username, string password)
        {
            return !String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password);
        }

        public async Task SignIn(string username, string password)
        {
            gitHubClient.Connection.Credentials = new Credentials(username, password);

            try
            {
                await gitHubClient.User.Current();

                //navigationService.NavigateToActivity<R>();
            }
            catch (AuthorizationException)
            {
                return;
            }
        }
    }
}
