namespace Blooso.ViewModels
{
    #region

    using System.Windows.Input;

    using Repositories;

    using Views;
    using Views.EditProfile;

    using Xamarin.Forms;

    #endregion

    public class MainMenuViewModel : BaseViewModel
    {
        public MainMenuViewModel()
        {
            _userRepo = UserRepository.GetRepository();
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
        }

        public ICommand LogUserOutCommand => new Command(LogUserOut);

        public ICommand GetMatchesCommand => new Command(GoToGetMatches);

        public ICommand EditProfileCommand => new Command(GoToEditProfile);

        private async void GoToGetMatches()
        {
            await Shell.Current.GoToAsync(nameof(MatchOverviewPage));
        }

        private async void LogUserOut()
        {
            _userRepo.SetCurrentlyLoggedInUser(0);

            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        private async void GoToEditProfile(object obj)
        {
            await Shell.Current.GoToAsync(nameof(EditProfilePage));
        }
    }
}