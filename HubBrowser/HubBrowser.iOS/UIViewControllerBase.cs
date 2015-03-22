using System;
using Caliburn.Micro;
using UIKit;

namespace HubBrowser.iOS
{
    public class UIViewControllerBase<T> : UIViewController, IUIViewController
    {
        public event EventHandler ViewLoaded = delegate { };
        public event EventHandler ViewAppeared = delegate { }; 

        public UIViewControllerBase()
        {
            SetupViewModel();
        }

        protected internal UIViewControllerBase(IntPtr handle) : base(handle)
        {
            SetupViewModel();
        }

        private void SetupViewModel()
        {
            ViewModel = IoC.Get<T>();

            var viewAware = ViewModel as IViewAware;

            if (viewAware != null)
            {
                viewAware.AttachView(this);
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var activate = ViewModel as IActivate;

            if (activate != null)
            {
                activate.Activate();
            }

            ViewLoaded(this, EventArgs.Empty);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            ViewAppeared(this, EventArgs.Empty);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            var deactivate = ViewModel as IDeactivate;

            if (deactivate != null)
            {
                deactivate.Deactivate(false);
            }
        }

        protected T ViewModel { get; private set; }
    }
}