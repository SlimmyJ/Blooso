namespace Blooso.ViewModels
{
    #region

    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Models;

    using Repositories;

    using Views;

    using Xamarin.Forms;

    #endregion

    public class MatchOverviewViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users;

        public MatchOverviewViewModel()
        {
            Users = new ObservableCollection<User>();

            // with database
            // _userRepo = new UserRepository();

            // with singleton
            _userRepo = UserRepository.GetRepository();

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

        public Command LoadUsersCommand => new(LoadUsers);

        public Command PerformSearchCommand =>
            new Command<string>(
                query => { Users = new ObservableCollection<User>(_userRepo.GetSearchResults(query)); });

        public Command<User> ItemTappedCommand => new(ItemTapped);

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.UserId)}={user.Id}");
        }

        public void LoadUsers()
        {
            IsBusy = true;

            // var users = _userRepo.GetAllUsers();
            List<User> users = _userRepo.GetMatchResults();
            Users = new ObservableCollection<User>(users);
            IsBusy = false;
        }
    }
}