namespace Blooso.ViewModels
{
    #region

    using System.Windows.Input;

    using Blooso.Models;
    using Blooso.Repositories;
    using Blooso.Views;

    using Xamarin.Forms;

    #endregion

    public class MainMenuViewModel : BaseViewModel
    {
        public MainMenuViewModel()
        {
            this.UserRepo = UserRepository.GetRepository();
            this.CurrentUser = this.UserRepo.GetCurrentlyLoggedInUser();
        }

        public new User CurrentUser { get; private set; }

        public ICommand EditProfileCommand
        {
            get
            {
                return new Command(this.EditProfile);
            }
        }

        public ICommand GetMatchesCommand
        {
            get
            {
                return new Command(this.GetMatches);
            }
        }

        public ICommand LogUserOutCommand
        {
            get
            {
                return new Command(this.LogUserOut);
            }
        }

        public User GetCurrentUser()
        {
            return this.CurrentUser;
        }

        private async void EditProfile(object obj)
        {
            await Shell.Current.GoToAsync("EditProfilePage");
        }

        private async void GetMatches()
        {
            await Shell.Current.GoToAsync(nameof(MatchOverviewPage));
        }

        private async void LogUserOut()
        {
            this.UserRepo.SetCurrentlyLoggedInUser(0);

            await Shell.Current.GoToAsync("LoginPage");
        }
    }
}