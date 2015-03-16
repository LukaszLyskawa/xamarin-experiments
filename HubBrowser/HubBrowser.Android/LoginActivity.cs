using System;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Widget;
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

            button.Click += (s, e) => ViewModel.SignIn(userName.Text, password.Text);

            EventHandler<TextChangedEventArgs> toggleSignIn = (s, e) =>
            {
                button.Enabled = ViewModel.CanSignIn(userName.Text, password.Text);
            };

            userName.TextChanged += toggleSignIn;
            password.TextChanged += toggleSignIn;
        }
    }
}