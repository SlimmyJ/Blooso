namespace Blooso.ViewModels
{
    #region

    using System.Windows.Input;

    using Repositories;

    using Xamarin.Forms;

    #endregion

    public class EditProfileViewModel : BaseViewModel
    {
        public EditProfileViewModel()
        {
            CurrentUser = _userRepo.CurrentlyLoggedInUser;
        }

        public ICommand EditActivityListCommand => new Command(EditActivityList);

        public ICommand EditUserTagsListCommand => new Command(EditUserTagsList);

        private async void EditActivityList(object obj)
        {
            await Shell.Current.GoToAsync("EditActivityListPage");
        }

        private async void EditUserTagsList(object obj)
        {
            await Shell.Current.GoToAsync("EditUserTagsListPage");
        }
    }
}