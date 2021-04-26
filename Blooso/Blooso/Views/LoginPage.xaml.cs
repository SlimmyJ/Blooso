﻿#region

using System;

using Blooso.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#endregion

namespace Blooso.Views
{
    #region

    #endregion

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var _ = new LoginViewModel();
            EntryUserLogin.Completed += (object sender, EventArgs e) => { EntryUserPassword.Focus(); };
        }

        protected override bool OnBackButtonPressed() => true;
    }
}