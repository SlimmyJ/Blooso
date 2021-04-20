using System;

using Blooso.Views;

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
            Routing.RegisterRoute(nameof(MatchOverviewPage), typeof(MatchOverviewPage));
            Routing.RegisterRoute(nameof(MatchDetailPage), typeof(MatchDetailPage));
        }
    }
}