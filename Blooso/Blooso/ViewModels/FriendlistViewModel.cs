namespace Blooso.ViewModels
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Blooso.Models;
    using Blooso.Repositories;
    using Blooso.Views;

    using Xamarin.Forms;

    #endregion

    public class FriendlistViewModel : BaseViewModel
    {
        private ObservableCollection<User> _friendList;

        public ObservableCollection<User> FriendList
        {
            get => this._friendList;
            set
            {
                this._friendList = value;
                this.OnPropertyChanged(nameof(this.FriendList));
            }
        }

        public Command LoadUsersCommand => new Command(this.LoadUsers);

        public Command<User> ItemTappedCommand => new Command<User>(this.ItemTapped);

        public Command<User> FriendListSwipeCommand => new Command<User>(this.SendMessageToUser);

        private async void SendMessageToUser(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(UserFeedPage)}?{nameof(UserFeedViewModel)}={user.Id}");
        }

        public FriendlistViewModel()
        {
            this._userRepo = UserRepository.GetRepository();
            ICollection<User> friends = this._userRepo.GetCurrentlyLoggedInUser().FriendList;
            this.FriendList = new ObservableCollection<User>(friends);
        }

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.UserId)}={user.Id}");
        }

        public void LoadUsers()
        {
            this.IsBusy = true;

            try
            {
                User currentUser = this._userRepo.GetCurrentlyLoggedInUser();
                if (currentUser.FriendList != null)
                {
                    foreach (User temp in currentUser.FriendList)
                    {
                        this.FriendList.Add(temp);
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
                this.IsBusy = false;
            }
        }
    }
}