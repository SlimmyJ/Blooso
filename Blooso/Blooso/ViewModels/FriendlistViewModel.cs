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
        private ObservableCollection<User> friendList;

        public ObservableCollection<User> FriendList
        {
            get => friendList;
            set
            {
                friendList = value;
                OnPropertyChanged(nameof(FriendList));
            }
        }

        private IUserRepository _userRepository;
        public ICommand LoadUsersCommand => new Command(LoadUsers);
        public Command<User> ItemTappedCommand => new Command<User>(ItemTapped);

        public FriendlistViewModel()
        {
            FriendList = new ObservableCollection<User>();
            _userRepository = UserRepository.GetRepository();
            LoadUsers();
        }

        public void LoadUsers()
        {
            IsBusy = true;

            try
            {
                var currentUser = _userRepository.GetCurrentlyLoggedInUser();
                if (currentUser.FriendsList != null)
                {
                    FriendList = new ObservableCollection<User>(currentUser.FriendsList);
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

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.UserId)}={user.Id}");
        }
    }
}