using System;

using Xamarin.Forms;

namespace Blooso
    {
    public partial class AppShell : Xamarin.Forms.Shell
        {
        private async void OnMenuItemClicked(object sender, EventArgs e)
            {
            await Shell.Current.GoToAsync("//LoginPage");
            }

        public AppShell()
            {
            InitializeComponent();
            }
        }
    }