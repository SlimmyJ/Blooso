#region

using System.Collections.Generic;
using System.Collections.ObjectModel;

using Blooso.Data;
using Blooso.Models;
using Blooso.Views;

using Xamarin.Forms;

#endregion

namespace Blooso.ViewModels
{
    #region

    #endregion

    public class MatchOverviewViewModel : BaseViewModel
    {
        private ICollection<User> _users;

        public MatchOverviewViewModel() : base(DummyData.Instance)
        {
            Users = new ObservableCollection<User>();
            _userRepo.GetAllUsers();

            LoadUsers();
        }

        public ObservableCollection<User> Users
        {
            get => _users as ObservableCollection<User>;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public Command LoadUsersCommand => new(LoadUsers);

        public Command PerformSearchCommand =>
            new Command<string>(query => Users = new ObservableCollection<User>(_userRepo.GetSearchResults(query)));

        public Command<User> ItemTappedCommand => new(ItemTapped);

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync(
                $"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.UserId)}={user.UserId}");
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