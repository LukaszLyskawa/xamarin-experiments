using System;

namespace HubBrowser.Core.Services
{
    public interface INavigationService
    {
        void NavigateToActivity<T>();
    }
}