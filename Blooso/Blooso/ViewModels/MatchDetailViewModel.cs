namespace Blooso.ViewModels
{
    #region

    using System;
    using System.Collections.ObjectModel;

    using Blooso.Interfaces;
    using Blooso.Models;
    using Blooso.Repositories;

    using Xamarin.Forms;

    #endregion

    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class MatchDetailViewModel : BaseViewModel
    {
        private User _userDetail;

        public User UserDetail
        {
            get => this._userDetail;
            set
            {
                this._userDetail = value;
                this.OnPropertyChanged(nameof(this.UserDetail));
            }
        }

        public string UserInput { get; set; }

        private ObservableCollection<Message> _userFeed;

        public ObservableCollection<Message> UserFeed
        {
            get => this._userFeed;
            set
            {
                this._userFeed = value;
                this.OnPropertyChanged(nameof(this.UserFeed));
            }
        }

        private int _userId;

        private IUserRepository userRepo;

        public MatchDetailViewModel()
        {
            this.UserDetail = new User();
            this.UserFeed = new ObservableCollection<Message>();
            this.userRepo = UserRepository.GetRepository();
        }

        public Command ActivityTappedAccount => new Command(this.ActivityTapped);

        public int UserId
        {
            get => this._userId;
            set
            {
                this._userId = value;
                this.LoadUser(value);
            }
        }

        public Command AddUserToFavouritesCommand => new Command(this.AddUserToFavourites);

        public Command OnPressSendMessage => new Command(this.SendMessageAsync);

        private async void SendMessageAsync()
        {
            var wallMessage = new Message
                              {
                                  Recipient = this.UserDetail,
                                  Text = this.UserInput,
                                  Author = this.userRepo.GetCurrentlyLoggedInUser(),
                                  IsPositiveReview = true,
                                  CreatedAt = DateTime.Now
                              };

            this.UserDetail.UserFeedMessages.Add(wallMessage);
            await this.userRepo.UpdateUser(this.UserDetail);
        }

        private void ActivityTapped()
        {
            Application.Current.MainPage.DisplayAlert("TODO", "List of activities", "OK");
        }

        private async void AddUserToFavourites()
        {
            User loggedInUser = this.userRepo.GetCurrentlyLoggedInUser();
            if (loggedInUser.Id != this.UserDetail.Id)
            {
                loggedInUser.FriendList.Add(this.UserDetail);
            }

            await Shell.Current.GoToAsync("..");
        }

        private void LoadUser(int value)
        {
            this.UserDetail = this.userRepo.GetUser(value);
            this.UserFeed = new ObservableCollection<Message>(this.UserDetail.UserFeedMessages);
        }
    }
}