using Blooso.Interfaces;
using Blooso.Models;
using Blooso.Repositories;
using Blooso.Views;

using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        private IUserRepository userRepo;
        public User CurrentUser { get; set; }
        public Command LoadUsersCommand => new Command(LoadUsers);

        public MainMenuViewModel()
        {
            userRepo = UserRepository.GetRepository();
            CurrentUser = userRepo.GetCurrentlyLoggedInUser();
        }

        private async void LoadUsers()
        {
            await Shell.Current.GoToAsync(nameof(MatchOverviewPage));
        }

        public User GetCurrentUser()
        {
            return CurrentUser;
        }
    }
}