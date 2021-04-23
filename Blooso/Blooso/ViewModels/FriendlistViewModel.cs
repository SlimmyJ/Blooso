namespace Blooso.ViewModels
{
    #region

    using System.Collections.ObjectModel;

    using Blooso.Models;
    using Blooso.Views;

    using Xamarin.Forms;

    #endregion

    public class FriendlistViewModel : BaseViewModel
    {
        private ObservableCollection<User> _friendList;

        public ObservableCollection<User> FriendList
        {
            get => this.CurrentUser.FriendList;

            set
            {
                this.CurrentUser.FriendList = value;
                this.OnPropertyChanged(nameof(this.FriendList));
            }
        }

        public Command<User> FriendListSwipeCommand => new Command<User>(this.SendMessageToUser);

        public FriendlistViewModel()
        {
            _userRepo = UserRepository.GetRepository();
            FriendList = _userRepo.GetCurrentlyLoggedInUser().FriendList;
        }

        private async void ItemTapped(User user)
        {
            await Shell.Current.GoToAsync(
                $"{nameof(MatchDetailPage)}?{nameof(MatchDetailViewModel.DetailedUserId)}={user.Id}");
        }

        private async void SendMessageToUser(User user)
        {
            IsBusy = true;

            try
            {
                var currentUser = _userRepo.GetCurrentlyLoggedInUser();
                if (currentUser.FriendList != null)
                {
                    foreach (var temp in currentUser.FriendList)
                    {
                        FriendList.Add(temp);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}