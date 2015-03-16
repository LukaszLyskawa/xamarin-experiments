using System;
using Caliburn.Micro;
using HubBrowser.Core.ViewModels;
using UIKit;

namespace HubBrowser.iOS
{
    public partial class LoginViewController : UIViewController
	{
		public LoginViewController (IntPtr handle) : base (handle)
		{
            ViewModel = IoC.Get<LoginViewModel>();

            var viewAware = ViewModel as IViewAware;

            if (viewAware != null)
            {
                viewAware.AttachView(this);
            }
		}

	    public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();

            SignIn.TouchUpInside += (s, e) => ViewModel.SignIn(Username.Text, Password.Text);

            EventHandler toggleSignIn = (s, e) =>
            {
                SignIn.Enabled = ViewModel.CanSignIn(Username.Text, Password.Text);
            };

            Username.EditingChanged += toggleSignIn;
            Password.EditingChanged += toggleSignIn;
	    }

        protected LoginViewModel ViewModel { get; private set; }
	}
}
