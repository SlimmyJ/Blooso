using System;
using System.Diagnostics;

using Blooso.Interfaces;
using Blooso.Models;
using Blooso.Repositories;

using Xamarin.Forms;

namespace Blooso.ViewModels
{
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class MatchDetailViewModel : BaseViewModel
    {
        private IUserRepository userRepo;

        private User userDetail;

        public Command ActivityTappedAccount => new Command(ActivityTapped);

        private void ActivityTapped()
        {
            Application.Current.MainPage.DisplayAlert("TODO", "List of activities", "OK"); ;
        }

        public User UserDetail
        {
            get
            {
                return userDetail;
            }
            set
            {
                userDetail = value;
                OnPropertyChanged(nameof(UserDetail));
            }
        }

        private int userId;

        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
                //load the correct user based on ID
                LoadUser(value);
            }
        }

        public Command AddUserToFavouritesCommand => new Command(AddUserToFavourites);

        public MatchDetailViewModel()
        {
            UserDetail = new User();
            userRepo = UserRepository.GetRepository();
        }

        private async void AddUserToFavourites()
        {
            var loggedInUser = userRepo.GetCurrentlyLoggedInUser();

            if (loggedInUser.Id != UserDetail.Id)
                loggedInUser.FriendsList.Add(UserDetail);

            await Shell.Current.GoToAsync("..");
        }

        private void LoadUser(int value)
        {
            try
            {
                UserDetail = userRepo.GetUser(value);
            }
            catch (Exception e)
            {
                Debug.WriteLine("failed to load item");
            }
        }
    }
}