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
        public User CurrentUser { get; set; }

        public ICommand LogUserOutCommand => new Command(LogUserOut);

        public ICommand GetMatchesCommand => new Command(GetMatches);

        public ICommand EditProfileCommand => new Command(EditProfile);

        public MainMenuViewModel()
        {
            this.UserRepo = UserRepository.GetRepository();
            this.CurrentUser = this.UserRepo.GetCurrentlyLoggedInUser();
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