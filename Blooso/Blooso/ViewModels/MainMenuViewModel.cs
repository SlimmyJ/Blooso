namespace Blooso.ViewModels
{
    #region

    using System.Windows.Input;

    using Blooso.Repositories;
    using Blooso.Views;

    using Xamarin.Forms;

    #endregion

    public class MainMenuViewModel : BaseViewModel
    {
        public ICommand LogUserOutCommand => new Command(this.LogUserOut);

        public ICommand GetMatchesCommand => new Command(this.GetMatches);

        public ICommand EditProfileCommand => new Command(this.EditProfile);

        public MainMenuViewModel()
        {
            this._userRepo = UserRepository.GetRepository();
            this.CurrentUser = this._userRepo.GetCurrentlyLoggedInUser();
        }

        private async void GetMatches()
        {
            // _userRepo.GetAllUsers();
            await Shell.Current.GoToAsync(nameof(MatchOverviewPage));
        }

        private async void LogUserOut()
        {
            this._userRepo.SetCurrentlyLoggedInUser(0);

            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        private async void EditProfile(object obj)
        {
            await Shell.Current.GoToAsync(nameof(EditProfilePage));
        }
    }
}