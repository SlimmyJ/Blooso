using System;
using System.Collections.ObjectModel;

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

        public string UserInput { get; set; }

        private ObservableCollection<Message> _userFeed;

        public ObservableCollection<Message> UserFeed
        {
            get => _userFeed;
            set
            {
                _userFeed = value;
                OnPropertyChanged(nameof(UserFeed));
            }
        }

        private int _userId;
        private IUserRepository userRepo;

        public MatchDetailViewModel()
        {
            UserDetail = new User();
            UserFeed = new ObservableCollection<Message>();
            userRepo = UserRepository.GetRepository();
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

        public Command OnPressSendMessage => new Command(SendMessage);

        private void SendMessage()
        {
            var wallMessage = new Message
            {
                Recipient = UserDetail,
                Text = UserInput,
                Author = userRepo.GetCurrentlyLoggedInUser(),
                IsPositiveReview = true,
                CreatedAt = DateTime.Now
            };

            UserDetail.UserFeedMessages.Add(wallMessage);
        }

        private void ActivityTapped()
        {
            Application.Current.MainPage.DisplayAlert("TODO", "List of activities", "OK");
        }

        private async void AddUserToFavourites()
        {
            var loggedInUser = userRepo.GetCurrentlyLoggedInUser();
            if (loggedInUser.Id != UserDetail.Id) loggedInUser.FriendList.Add(UserDetail);
            await Shell.Current.GoToAsync("..");
        }

        private void LoadUser(int value)
        {
            UserDetail = userRepo.GetUser(value);
            UserFeed = new ObservableCollection<Message>(UserDetail.UserFeedMessages);
        }
    }
}