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

        private IUserRepository _userRepository;

        public MatchOverviewViewModel()
        {
            Users = new ObservableCollection<User>();
            _userRepository = UserRepository.GetRepository();
            LoadUsers();
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public Command LoadUsersCommand => new Command(LoadUsers);

        public Command PerformSearchCommand => new Command<string>(query =>
        {
            Users = new ObservableCollection<User>(_userRepository.GetSearchResults(query));
        });

        public Command<User> ItemTappedCommand => new Command<User>(ItemTapped);

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.UserId)}={user.Id}");
        }

        public void LoadUsers()
        {
            IsBusy = true;
            var users = _userRepository.GetMatchResults();
            Users = new ObservableCollection<User>(users);
            IsBusy = false;
        }
    }
}