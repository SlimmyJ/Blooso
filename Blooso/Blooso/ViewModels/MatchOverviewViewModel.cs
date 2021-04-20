using System;
using System.Collections.ObjectModel;

using Blooso.Interfaces;
using Blooso.Models;
using Blooso.Repositories;
using Blooso.Views;
using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class MatchOverviewViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private IUserRepository _userRepository;

        public MatchOverviewViewModel()
        {
            Users = new ObservableCollection<User>();
            _userRepository = UserRepository.GetRepository();
            LoadUsers();
        }

        public Command LoadUsersCommand => new Command(LoadUsers);
        public Command<User> ItemTappedCommand => new Command<User>(ItemTapped);


        public void LoadUsers()
        {
            IsBusy = true;

            try
            {
                var users = _userRepository.GetAllUsers();
                Users = new ObservableCollection<User>(users);
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