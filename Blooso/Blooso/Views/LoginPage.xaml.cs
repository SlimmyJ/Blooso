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
        public LoginViewModel loginVM;

        public LoginPage()
        {
            InitializeComponent();
            loginVM = new LoginViewModel();
            EntryUserLogin.Completed += (object sender, EventArgs e) => { EntryUserPassword.Focus(); };

            // EntryUserPassword.Completed += (object sender, EventArgs e) =>
            // {
            // loginVM.SubmitCommand.Execute(null);
            // };
        }

        protected override bool OnBackButtonPressed() => true;
    }
}