using System;

using Blooso.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Blooso.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendlistPage : ContentPage
    {
        private FriendlistViewModel _friendlistViewModel;

        public FriendlistPage()
        {
            _friendlistViewModel = new FriendlistViewModel();
            InitializeComponent();
        }

        private async void OnFavoriteSwipeItemInvoked(object sender, EventArgs e)
        {
            await DisplayAlert("SwipeView", "Favorite invoked.", "OK");
        }

        private async void OnShareSwipeItemInvoked(object sender, EventArgs e)
        {
            await DisplayAlert("SwipeView", "Share invoked.", "OK");
        }

        private async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
            await DisplayAlert("SwipeView", "Delete invoked.", "OK");
        }
    }
}