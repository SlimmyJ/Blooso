namespace Blooso.ViewModels
{
    #region

    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Blooso.Models;
    using Blooso.Repositories;
    using Blooso.Views;

    using Xamarin.Forms;

    #endregion

    public class MatchOverviewViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get => this._users;
            set
            {
                this._users = value;
                this.OnPropertyChanged(nameof(this.Users));
            }
        }

        public MatchOverviewViewModel()
        {
            this.Users = new ObservableCollection<User>();

            // with database
            // _userRepo = new UserRepository();

            // with singleton
            this._userRepo = UserRepository.GetRepository();

            this.LoadUsers();
        }

        public Command LoadUsersCommand => new Command(this.LoadUsers);

        public Command PerformSearchCommand =>
            new Command<string>(
                query => { this.Users = new ObservableCollection<User>(this._userRepo.GetSearchResults(query)); });

        public Command<User> ItemTappedCommand => new Command<User>(this.ItemTapped);

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.UserId)}={user.Id}");
        }

        public void LoadUsers()
        {
            this.IsBusy = true;

            // var users = _userRepo.GetAllUsers();
            List<User> users = this._userRepo.GetMatchResults();
            this.Users = new ObservableCollection<User>(users);
            this.IsBusy = false;
        }
    }
}