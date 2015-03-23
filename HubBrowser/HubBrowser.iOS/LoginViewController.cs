using System;
using HubBrowser.Core.Extensions;
using HubBrowser.Core.ViewModels;

namespace HubBrowser.iOS
{
    public partial class LoginViewController : UIViewControllerBase<LoginViewModel>
	{
		public LoginViewController (IntPtr handle) 
            : base (handle)
		{
            
		}

	    public override void ViewDidLoad()
	    {
	        base.ViewDidLoad();

	        SignIn.TouchUpInside += async (s, e) => await ViewModel.SignIn();

            EventHandler toggleSignIn = (s, e) =>
            {
                ViewModel.Username = Username.Text;
                ViewModel.Password = Password.Text;

                SignIn.Enabled = ViewModel.CanSignIn;
            };

            Username.EditingChanged += toggleSignIn;
            Password.EditingChanged += toggleSignIn;

            ViewModel.OnChanged(v => v.Feedback, () => Feedback.Text = ViewModel.Feedback);
	    }
	}
}
