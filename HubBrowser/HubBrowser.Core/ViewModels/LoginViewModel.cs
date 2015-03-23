using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using HubBrowser.Core.Services;
using Octokit;
using PropertyChanged;

namespace HubBrowser.Core.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IGitHubClient gitHubClient;
        private readonly IApplicationNavigationService navigate;

        public LoginViewModel(IGitHubClient gitHubClient, IApplicationNavigationService navigate)
        {
            this.gitHubClient = gitHubClient;
            this.navigate = navigate;
        }

        public string Username
        {
            get; set;
        }

        public void OnUsernameChanged()
        {
            Feedback = String.Empty;
        }

        public string Password
        {
            get; set;
        }

        public void OnPasswordChanged()
        {
            Feedback = String.Empty;
        }

        public string Feedback
        {
            get; set;
        }

        [DependsOn("Username", "Password")]
        public bool CanSignIn
        {
            get { return !String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password); }
        }

        public async Task SignIn()
        {
            navigate.ToRepositoryList();

            //gitHubClient.Connection.Credentials = new Credentials(Username, Password);

            //try
            //{
            //    await gitHubClient.User.Current();

            //    navigate.ToRepositoryList();
            //}
            //catch (AuthorizationException)
            //{
            //    Feedback = "Invalid credentials";
            //}
        }
    }
}
