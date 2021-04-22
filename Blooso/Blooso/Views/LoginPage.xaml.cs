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
        public LoginViewModel LoginVm;

        public LoginPage()
        {
            this.InitializeComponent();
            this.LoginVm = new LoginViewModel();
            this.EntryUserLogin.Completed += (object sender, EventArgs e) => { this.EntryUserPassword.Focus(); };

            this.EntryUserLogin.Completed += (object sender, EventArgs e) =>
                {
                    this.LoginVm.SubmitCommand.Execute(null);
                };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}