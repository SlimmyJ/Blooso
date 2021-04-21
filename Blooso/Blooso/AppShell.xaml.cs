using System;
using Blooso.Views;
using Xamarin.Forms;

namespace Blooso
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MatchOverviewPage), typeof(MatchOverviewPage));
            Routing.RegisterRoute(nameof(MatchDetailPage), typeof(MatchDetailPage));
            Routing.RegisterRoute(nameof(MainMenuPage), typeof(MainMenuPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }
    }
}