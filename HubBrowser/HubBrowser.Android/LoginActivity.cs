using System;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Widget;
using HubBrowser.Core.Extensions;
using HubBrowser.Core.ViewModels;

namespace HubBrowser.Android
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : ActivityBase<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Login);

            var userName = FindViewById<EditText>(Resource.Id.userName);
            var password = FindViewById<EditText>(Resource.Id.password);

            var button = FindViewById<Button>(Resource.Id.signIn);

            var feedback = FindViewById<TextView>(Resource.Id.feedback);

            button.Click += async (s, e) => await ViewModel.SignIn();

            EventHandler<TextChangedEventArgs> toggleSignIn = (s, e) =>
            {
                ViewModel.Username = userName.Text;
                ViewModel.Password = password.Text;

                button.Enabled = ViewModel.CanSignIn;
            };

            userName.TextChanged += toggleSignIn;
            password.TextChanged += toggleSignIn;

            ViewModel.OnChanged(v => v.Feedback, () => feedback.Text = ViewModel.Feedback);
        }
    }
}