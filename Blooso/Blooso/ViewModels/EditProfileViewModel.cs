namespace Blooso.ViewModels
{
    #region

    using System.Windows.Input;

    using Blooso.Repositories;

    using Xamarin.Forms;

    #endregion

    public class EditProfileViewModel : BaseViewModel
    {
        public EditProfileViewModel()
        {
            this.UserRepo = UserRepository.GetRepository();
            this.CurrentUser = this.UserRepo.GetCurrentlyLoggedInUser();
        }

        public ICommand EditActivityListCommand => new Command(this.EditActivityList);

        public ICommand EditUserTagsListCommand => new Command(this.EditUserTagsList);

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