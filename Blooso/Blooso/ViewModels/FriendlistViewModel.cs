using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Blooso.Interfaces;
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

        private async void SendMessageToUser(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(UserFeedPage)}?{nameof(UserFeedViewModel)}={user.Id}");
        }

        public FriendlistViewModel()
        {
            _userRepo = UserRepository.GetRepository();
            FriendList = _userRepo.GetCurrentlyLoggedInUser().FriendList;
        }

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.UserId)}={user.Id}");
        }

        public void LoadUsers()
        {
            IsBusy = true;

            try
            {
                var currentUser = _userRepo.GetCurrentlyLoggedInUser();
                if (currentUser.FriendList != null)
                {
                    foreach (var temp in currentUser.FriendList)
                    {
                        FriendList.Add(temp);
                    }
                }
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