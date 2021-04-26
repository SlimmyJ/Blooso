#region

using System.Windows.Input;

using Blooso.Data;
using Blooso.Views.EditProfile;

using Xamarin.Forms;

#endregion

namespace Blooso.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        public EditProfileViewModel() : base(DummyData.Instance) => CurrentUser = _userRepo.CurrentlyLoggedInUser;

        public ICommand EditActivityListCommand => new Command(EditActivityList);

        public ICommand EditUserTagsListCommand => new Command(EditUserTagsList);

        public ICommand SaveProfileCommand => new Command(SaveProfile);

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