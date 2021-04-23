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

        public MatchOverviewViewModel()
        {
            this.UserRepo = UserRepository.GetRepository();
            this.MatchesObservableCollection = new ObservableCollection<User>(this.UserRepo.GetMatchResults());
        }

        public ObservableCollection<User> MatchesObservableCollection
        {
            get
            {
                return this.MatchesObservableCollection;
            }

            set
            {
                this.MatchesObservableCollection = value;
                this.OnPropertyChanged(nameof(this.MatchesObservableCollection));
            }
        }

        public ICommand OnSingleTapUserInOverViewCommand
        {
            get
            {
                return new Command<User>(this.ItemTapped);
            }
        }

        public Command PerformSearchCommand
        {
            get
            {
                return new Command<string>(
                    query => this.MatchesObservableCollection =
                                 new ObservableCollection<User>(this.UserRepo.GetSearchResults(query)));
            }
        }

        public async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync(
                $"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.DetailedUserId)}={user.Id}");
        }
    }
}