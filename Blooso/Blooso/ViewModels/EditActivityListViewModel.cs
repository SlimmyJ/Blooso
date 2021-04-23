namespace Blooso.ViewModels
{
    #region

    using System.Collections.ObjectModel;

    using Blooso.Interfaces;
    using Blooso.Repositories;

    #endregion

    public class EditActivityListViewModel : BaseViewModel
    {
        private ObservableCollection<Activities> _activities;

        private IUserRepository _userRepo;

        public EditActivityListViewModel()
        {
            this.Activities = new ObservableCollection<Activities>();
            this._userRepo = UserRepository.GetRepository();
            this.CurrentUser = this._userRepo.GetCurrentlyLoggedInUser();
            this.GetUserActivities();
        }

        public ObservableCollection<Activities> Activities
        {
            get => this._activities;

            set
            {
                this._activities = value;
                this.OnPropertyChanged(nameof(this.Activities));
            }
        }

        public void GetUserActivities()
        {
            var activities = this.CurrentUser.ActivityList;
            this.Activities = new ObservableCollection<Activities>(activities);
        }
    }
}