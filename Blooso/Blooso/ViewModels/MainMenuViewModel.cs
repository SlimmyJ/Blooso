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
        private readonly IUserRepository userRepo;

        public MainMenuViewModel()
        {
            userRepo = UserRepository.GetRepository();
            CurrentUser = userRepo.GetCurrentlyLoggedInUser();
        }

        public User CurrentUser { get; set; }
        public ICommand LoadUsersCommand => new Command(LoadUsers);
        public ICommand LogUserOutCommand => new Command(LogUserOut);

        private async void LoadUsers()
        {
            await Shell.Current.GoToAsync(nameof(MatchOverviewPage));
        }

        public User GetCurrentUser()
        {
            return CurrentUser;
        }

        private async void LogUserOut()
        {
            userRepo.SetCurrentlyLoggedInUser(0);

            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}