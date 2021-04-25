using System;
using System.Collections.ObjectModel;
using Blooso.Models;
using Blooso.Repositories;
using Blooso.Views;
using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class FriendlistViewModel : BaseViewModel
    {
        private ObservableCollection<User> _friendList;

        public ObservableCollection<User> FriendList
        {
            get => _friendList;
            set
            {
                _friendList = value;
                OnPropertyChanged(nameof(FriendList));
            }
        }

        public Command LoadUsersCommand => new Command(LoadUsers);
        public Command<User> ItemTappedCommand => new Command<User>(ItemTapped);

        public Command<User> FriendListSwipeCommand => new Command<User>(SendMessageToUser);
        public Command<User> DeleteFriendCommand => new Command<User>(DeleteFriend);

        private async void SendMessageToUser(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(UserFeedPage)}?{nameof(UserFeedViewModel)}={user.Id}");
        }

        public FriendlistViewModel()
        {
            _userRepo = UserRepository.GetRepository();
            FriendList = _userRepo.GetCurrentlyLoggedInUser().FriendList;
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
        }

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.UserId)}={user.Id}");
        }

        private void DeleteFriend(User user)
        {
            CurrentUser.FriendList.Remove(user);
        }

        public void LoadUsers()
        {
            IsBusy = true;

            try
            {
                var currentUser = _userRepo.GetCurrentlyLoggedInUser();
                if (currentUser.FriendList == null) return;
                foreach (var temp in currentUser.FriendList) FriendList.Add(temp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}