using Blooso.Interfaces;
using Blooso.Models;
using Blooso.Repositories;

using Xamarin.Forms;

namespace Blooso.ViewModels
{
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class MatchDetailViewModel : BaseViewModel
    {
        private User _userDetail;

        public User UserDetail
        {
            get => _userDetail;
            set
            {
                _userDetail = value;
                OnPropertyChanged(nameof(UserDetail));
            }
        }

        private int _userId;
        public IUserRepository UserRepo;

        public MatchDetailViewModel()
        {
            UserDetail = new User();
            UserRepo = UserRepository.GetRepository();
        }

        public Command ActivityTappedAccount => new Command(ActivityTapped);

        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                LoadUser(value);
            }
        }

        public Command AddUserToFavouritesCommand => new Command(AddUserToFavourites);

        private void ActivityTapped()
        {
            Application.Current.MainPage.DisplayAlert("TODO", "List of activities", "OK");
        }

        private async void AddUserToFavourites()
        {
            var loggedInUser = UserRepo.GetCurrentlyLoggedInUser();
            if (loggedInUser.Id != UserDetail.Id) loggedInUser.FriendList.Add(UserDetail);
            await Shell.Current.GoToAsync("..");
        }

        private void LoadUser(int value)
        {
            UserDetail = UserRepo.GetUser(value);
        }
    }
}