using System;
using Blooso.Views;
using Blooso.Views.EditProfile;
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
            Routing.RegisterRoute(nameof(MainMenuPage), typeof(MainMenuPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
            Routing.RegisterRoute(nameof(EditActivityListPage), typeof(EditActivityListPage));
            Routing.RegisterRoute(nameof(EditUserTagsListPage), typeof(EditUserTagsListPage));
        }
    }
}