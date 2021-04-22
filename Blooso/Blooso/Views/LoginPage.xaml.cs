using System;

using Blooso.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Blooso.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel loginVM;

        public LoginPage()
        {
            InitializeComponent();
            loginVM = new LoginViewModel();
            EntryUserLogin.Completed += (object sender, EventArgs e) =>
            {
                EntryUserPassword.Focus();
            };

            EntryUserLogin.Completed += (object sender, EventArgs e) =>
            {
                loginVM.SubmitCommand.Execute(null);
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}