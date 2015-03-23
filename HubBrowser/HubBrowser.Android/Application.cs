using System;
using System.Collections.Generic;
using Android.App;
using Android.Runtime;
using Caliburn.Micro;
using HubBrowser.Android.Services;
using HubBrowser.Core.Services;
using HubBrowser.Core.ViewModels;
using Octokit;

namespace HubBrowser.Android
{
    [Application(Label = "@string/ApplicationName", Theme = "@style/CustomHoloTheme", Icon="@drawable/Icon")]
    public class Application : CaliburnApplication
    {
        private SimpleContainer container;

        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();

            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container
                .Instance<IGitHubClient>(new GitHubClient(new ProductHeaderValue("Hub.Browser.Android", "1.0.0")))
                .Instance<IApplicationNavigationService>(new ApplicationNavigationService(this));

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
    }
}