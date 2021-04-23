using System.Windows.Input;

using Blooso.Interfaces;
using Blooso.Models;
using Blooso.Repositories;
using Blooso.Views;

using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        public User CurrentUser { get; set; }

        public ICommand LogUserOutCommand => new Command(LogUserOut);

        public ICommand GetMatchesCommand => new Command(GetMatches);

        public ICommand EditProfileCommand => new Command(EditProfile);

        public MainMenuViewModel()
        {
            _userRepo = UserRepository.GetRepository();
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
        }

        private async void GetMatches()
        {
            _userRepo.GetMatchResults();
            await Shell.Current.GoToAsync(nameof(MatchOverviewPage));
        }

        private async void LogUserOut()
        {
            _userRepo.SetCurrentlyLoggedInUser(0);

            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        private async void EditProfile(object obj)
        {
            await Shell.Current.GoToAsync(nameof(EditProfilePage));
        }
    }
}