namespace Blooso.ViewModels
{
    #region

    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Blooso.Interfaces;
    using Blooso.Models;
    using Blooso.Repositories;
    using Blooso.Views;

    using Xamarin.Forms;

    #endregion

    public class MatchOverviewViewModel : BaseViewModel
    {
        private new IUserRepository UserRepo;

        public ObservableCollection<User> Users
        {
            get => this.MatchesObservableCollection;

            set
            {
                this.MatchesObservableCollection = value;
                this.OnPropertyChanged(nameof(this.MatchesObservableCollection));
            }
        }

        public MatchOverviewViewModel()
        {
            Users = new ObservableCollection<User>();
            _userRepo = new UserRepository();
            _userRepo = Repositories.UserRepository.GetRepository();
            LoadUsers();
        }

        public ICommand OnSingleTapUserInOverViewCommand => new Command<User>(this.ItemTapped);

        public Command PerformSearchCommand
        {
            Users = new ObservableCollection<User>(_userRepo.GetSearchResults(query));
        });

        public Command<User> ItemTappedCommand => new Command<User>(ItemTapped);

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.UserId)}={user.Id}");
        }

        public async void ItemTapped(User user)
        {
            IsBusy = true;
            var users = _userRepo.GetMatchResults();
            Users = new ObservableCollection<User>(users);
            IsBusy = false;
        }
    }
}