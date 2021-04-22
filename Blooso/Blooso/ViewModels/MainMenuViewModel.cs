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
        private readonly IUserRepository _userRepo;

        public MainMenuViewModel()
        {
            _userRepo = UserRepository.GetRepository();
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
        }

        public User CurrentUser { get; set; }

        public ICommand LogUserOutCommand
        {
            get { return new Command(LogUserOut); }
        }

        public ICommand GetMatchesCommand
        {
            get { return new Command(GetMatches); }
        }

        private async void GetMatches()
        {
            _userRepo.GetMatchResults();
            await Shell.Current.GoToAsync(nameof(MatchOverviewPage));
        }

        public User GetCurrentUser()
        {
            return CurrentUser;
        }

        private async void LogUserOut()
        {
            _userRepo.SetCurrentlyLoggedInUser(0);

            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}