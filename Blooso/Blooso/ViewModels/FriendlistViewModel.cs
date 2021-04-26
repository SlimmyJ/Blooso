using Blooso.Data.Repositories;

namespace Blooso.ViewModels
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Models;

    using Views;

    using Xamarin.Forms;

    #endregion

    public class FriendlistViewModel : BaseViewModel
    {
        private ICollection<User> _friendList;

        public FriendlistViewModel()
        {
            _userRepo = new UserRepository();
            ICollection<User> friends = _userRepo.GetCurrentlyLoggedInUser().FriendList;
            FriendList = new ObservableCollection<User>(friends);
        }

        public ICollection<User> FriendList
        {
            get => _friendList;
            set
            {
                _friendList = value;
                OnPropertyChanged(nameof(FriendList));
            }
        }

        public Command LoadUsersCommand => new(LoadUsers);

        public Command<User> ItemTappedCommand => new(ItemTapped);

        public Command<User> DeleteFriendCommand => new(DeleteFriend);

        public Command<User> FriendListSwipeCommand => new(SendMessageToUser);

        private async void SendMessageToUser(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(UserFeedPage)}?{nameof(UserFeedViewModel)}={user.UserId}");
        }

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync(
                $"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.UserId)}={user.UserId}");
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
                User currentUser = _userRepo.GetCurrentlyLoggedInUser();
                if (currentUser.FriendList == null)
                {
                    return;
                }

                foreach (User temp in currentUser.FriendList)
                {
                    FriendList.Add(temp);
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