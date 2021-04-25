using System.Windows.Input;
using Blooso.Repositories;
using Blooso.Views.EditProfile;
using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        public ICommand EditActivityListCommand => new Command(EditActivityList);
        public ICommand EditUserTagsListCommand => new Command(EditUserTagsList);
        public ICommand SaveProfileCommand => new Command(SaveProfile);

        public EditProfileViewModel()
        {
            _userRepo = UserRepository.GetRepository();
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
        }

        private async void EditActivityList(object obj)
        {
            await Shell.Current.GoToAsync(nameof(EditActivityListPage));
        }

        private async void EditUserTagsList(object obj)
        {
            await Shell.Current.GoToAsync(nameof(EditUserTagsListPage));
        }

        private async void SaveProfile(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}