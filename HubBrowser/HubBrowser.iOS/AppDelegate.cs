using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Foundation;
using HubBrowser.Core.Services;
using HubBrowser.Core.ViewModels;
using HubBrowser.iOS.Services;
using Octokit;
using UIKit;

namespace HubBrowser.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : CaliburnApplicationDelegate
    {
        private SimpleContainer container;

        public override UIWindow Window
        {
            get;
            set;
        }

        protected override void Configure()
        {
            var navigationController = (UINavigationController) Window.RootViewController;

            container = new SimpleContainer();

            container
                .Instance<IGitHubClient>(new GitHubClient(new ProductHeaderValue("Hub.Browser.iOS", "1.0.0")))
                .Instance<IApplicationNavigationService>(new ApplicationNavigationService(navigationController));

            container
                .PerRequest<LoginViewModel>();
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

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Initialize();

            return true;
        }
    }
}