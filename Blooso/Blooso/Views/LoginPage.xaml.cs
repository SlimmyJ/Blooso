namespace Blooso.Views
{
    #region

    using System;

    using Blooso.ViewModels;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    #endregion

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel loginViewModel;

        public LoginPage()
        {
            this.InitializeComponent();
            this.loginViewModel = new LoginViewModel();
            this.EntryUserLogin.Completed += (object sender, EventArgs e) => { this.EntryUserPassword.Focus(); };

            this.EntryUserLogin.Completed += (object sender, EventArgs e) =>
            {
                this.loginViewModel.SubmitCommand.Execute(null);
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}