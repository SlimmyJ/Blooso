using System;
using System.Collections.ObjectModel;

using Blooso.Interfaces;
using Blooso.Models;
using Blooso.Repositories;
using Blooso.Views;

using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class FriendlistViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;
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

        public FriendlistViewModel()
        {
            _userRepository = UserRepository.GetRepository();
            FriendList = _userRepository.GetCurrentlyLoggedInUser().FriendList;
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
                var currentUser = _userRepository.GetCurrentlyLoggedInUser();
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