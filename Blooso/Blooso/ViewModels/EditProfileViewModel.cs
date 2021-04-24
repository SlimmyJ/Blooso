namespace Blooso.ViewModels
{
    #region

    using System.Windows.Input;

    using Blooso.Repositories;

    using Xamarin.Forms;

    #endregion

    public class EditProfileViewModel : BaseViewModel
    {
        public ICommand EditActivityListCommand => new Command(this.EditActivityList);

        public ICommand EditUserTagsListCommand => new Command(this.EditUserTagsList);

        public EditProfileViewModel()
        {
            this._userRepo = UserRepository.GetRepository();
            this.CurrentUser = this._userRepo.GetCurrentlyLoggedInUser();
        }

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