namespace Blooso.ViewModels
{
    #region

    using System.Windows.Input;
    using System.Collections.ObjectModel;

    using Blooso.Interfaces;
    using Blooso.Models;
    using Blooso.Repositories;
    using Blooso.Views;

    using Xamarin.Forms;

    #endregion

    public class MatchOverviewViewModel : BaseViewModel
    {
        public MatchOverviewViewModel()
        {
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
                    execute: delegate (string query)
                        {
                            this.MatchesObservableCollection = new ObservableCollection<User>(this.UserRepo.GetSearchResults(query));
                        });
            }
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

        public async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.DetailedUserId)}={user.Id}");
        }
    }
}