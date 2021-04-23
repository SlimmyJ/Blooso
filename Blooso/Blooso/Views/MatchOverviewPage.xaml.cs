namespace Blooso.Views
{
    #region

    using System;

    using Blooso.Interfaces;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    using static Blooso.Repositories.UserRepository;

    #endregion

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchOverviewPage : ContentPage
    {
        public MatchOverviewPage()
        {
            this.InitializeComponent();
        }

        public void MatchOverviewPage_OnAppearing(object sender, EventArgs e)
        {
            GetRepository().GetMatchResults();
        }
    }
}