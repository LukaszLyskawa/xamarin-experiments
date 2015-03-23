using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using HubBrowser.Core.Services;
using HubBrowser.Core.ViewModels;
using HubBrowser.WinPhone.Services;
using HubBrowser.WinPhone.Views;
using Octokit;


namespace HubBrowser.WinPhone
{
    public sealed partial class App
    {
        private WinRTContainer container;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            container = new WinRTContainer();
            container.RegisterWinRTServices();

            ViewModelLocator.AddNamespaceMapping("HubBrowser.WinPhone.Views", "HubBrowser.Core.ViewModels");

            container
                .Instance<IGitHubClient>(new GitHubClient(new ProductHeaderValue("Hub.Browser.WinPhone", "1.0.0")));

            container
                .PerRequest<LoginViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            var navigationService = container.RegisterNavigationService(rootFrame);

            container.Instance<IApplicationNavigationService>(new ApplicationNavigationService(navigationService));
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            DisplayRootView<LoginView>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                GetType().GetTypeInfo().Assembly,
                typeof (LoginViewModel).GetTypeInfo().Assembly
            };
        }
    }
}