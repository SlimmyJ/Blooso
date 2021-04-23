namespace Blooso.ViewModels
{
    #region

    using System.Collections.ObjectModel;

    using Blooso.Models;
    using Blooso.Views;

    using Xamarin.Forms;

    #endregion

    public class FriendlistViewModel : BaseViewModel
    {
        public FriendlistViewModel()
        {
        }

        public ObservableCollection<User> FriendList
        {
            get
            {
                return this.CurrentUser.FriendList;
            }

            set
            {
                this.CurrentUser.FriendList = value;
                this.OnPropertyChanged(nameof(this.FriendList));
            }
        }

        public Command<User> FriendListSwipeCommand
        {
            get
            {
                return new Command<User>(this.SendMessageToUser);
            }
        }

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync(
                $"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.DetailedUserId)}={user.Id}");
        }

        private async void SendMessageToUser(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(UserFeedPage)}?{nameof(UserFeedViewModel)}={user.Id}");
        }
    }
}