using System.Collections.Generic;

namespace Blooso.ViewModels
{
    #region

    using System;
    using System.Collections.ObjectModel;

    using Interfaces;

    using Models;

    using Repositories;

    using Xamarin.Forms;

    #endregion

    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class MatchDetailViewModel : BaseViewModel
    {
        private User _userDetail;

        private IEnumerable<Message> _userFeed;

        private int _userId;

        private IUserRepository userRepo;

        public MatchDetailViewModel()
        {
            UserDetail = new User();
            UserFeed = new ObservableCollection<Message>();
            userRepo = UserRepository.GetRepository();
        }

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

        public ICollection<Message> UserFeed
        {
            get => _userFeed as ObservableCollection<Message>;
            set
            {
                _userFeed = value;
                OnPropertyChanged(nameof(UserFeed));
            }
        }

        public Command ActivityTappedAccount => new(ActivityTapped);

        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                LoadUser(value);
            }
        }

        public Command AddUserToFavouritesCommand => new(AddUserToFavourites);

        public Command OnPressSendMessage => new(SendMessageAsync);

        private async void SendMessageAsync()
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
            await userRepo.UpdateUser(UserDetail);
        }

        private void ActivityTapped()
        {
            Application.Current.MainPage.DisplayAlert("TODO", "List of activities", "OK");
        }

        private async void AddUserToFavourites()
        {
            User loggedInUser = userRepo.GetCurrentlyLoggedInUser();
            if (loggedInUser.Id != UserDetail.Id)
            {
                loggedInUser.FriendList.Add(UserDetail);
            }

            await Shell.Current.GoToAsync("..");
        }

        private void LoadUser(int value)
        {
            UserDetail = userRepo.GetUser(value);
            UserFeed = new ObservableCollection<Message>(UserDetail.UserFeedMessages);
        }
    }
}