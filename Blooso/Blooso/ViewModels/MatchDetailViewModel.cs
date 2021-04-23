namespace Blooso.ViewModels
{
    #region

    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Blooso.Interfaces;
    using Blooso.Models;
    using Blooso.Repositories;

    using Xamarin.Forms;

    #endregion

    [QueryProperty(nameof(DetailedUserId), nameof(DetailedUserId))]
    public class MatchDetailViewModel : BaseViewModel
    {
        private int _detailedUserId;

        private new IUserRepository UserRepo;

        public MatchDetailViewModel()
        {
            this.DetailUserFeed = new ObservableCollection<Message>();
            this.UserRepo = UserRepository.GetRepository();
        }

        public ICommand AddUserToFavouritesCommand
        {
            get
            {
                return new Command(this.AddUserToFavourites);
            }
        }

        public new User DetailedUser { get; set; }

        public int DetailedUserId
        {
            get
            {
                return this._detailedUserId;
            }

            set
            {
                this._detailedUserId = value;
                this.DetailedUser = this.UserRepo.GetUser(value);
            }
        }

        public ICommand OnPressSendMessage
        {
            get
            {
                return new Command(this.SendMessage);
            }
        }

        public string UserInput { get; set; }

        private async void AddUserToFavourites()
        {
            var loggedInUser = this.UserRepo.GetCurrentlyLoggedInUser();
            if (loggedInUser.Id == this.DetailedUser.Id)
            {
            }
            else
            {
                loggedInUser.FriendList.Add(this.DetailedUser);
            }

            await Shell.Current.GoToAsync("..");
        }

        private void SendMessage()
        {
            var wallmessage = new Message
            {
                Recipient = this.DetailedUser,
                Text = this.UserInput,
                Author = this.UserRepo.GetCurrentlyLoggedInUser(),
                IsPositiveReview = true,
                CreatedAt = DateTime.Now
            };

            this.DetailedUser.UserFeedMessages.Add(wallmessage);
        }
    }
}