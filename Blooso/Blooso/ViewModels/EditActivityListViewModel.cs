using System;
using System.Collections.ObjectModel;
using System.Linq;
using Blooso.Models;
using Blooso.Repositories;
using Xamarin.Forms;

    #endregion

    public class EditActivityListViewModel : BaseViewModel
    {
        private ObservableCollection<Activity> _activities;

        public ObservableCollection<Activity> Activities
        {
            get => this._activities;
            set
            {
                this._activities = value;
                this.OnPropertyChanged(nameof(this.Activities));
            }
        }

        private ObservableCollection<Activity> _userActivities;

        public ObservableCollection<Activity> UserActivities
        {
            get => this._userActivities;
            set
            {
                this._userActivities = value;
                this.OnPropertyChanged(nameof(this.UserActivities));
            }
        }

        public Command<Activity> AddToActivityListCommand => new Command<Activity>(AddToActivityList);
        public Command<Activity> DeleteActivityFromListCommand => new Command<Activity>(DeleteActivityFromList);

        public void GetUserActivities()
        {
            Activities = new ObservableCollection<Activity>();
            UserActivities = new ObservableCollection<Activity>();
            _userRepo = UserRepository.GetRepository();
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
            GetUserActivities();
            GetAllActivities();
        }

        private void AddToActivityList(Activity activity)
        {
            if (this.UserActivities.Contains(activity))
            {
                Application.Current.MainPage.DisplayAlert(
                    "Glitch in the matrix",
                    "You can only have one of each as your favorite activity",
                    "OK");
            }

            this.UserActivities.Add(activity);
        }

        private void DeleteActivityFromList(Activity activity)
        {
            if (this.UserActivities.Count != 1)
            {
                this.UserActivities.Remove(activity);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Hold up!", "You need at least one activity", "OK");
        }

        public void GetUserActivities()
        {
            var activities = CurrentUser.Activities;
            UserActivities = new ObservableCollection<Activity>(activities);
        }

        public void GetAllActivities()
        {
            var activities = _userRepo.GetAllActivities();
            Activities = new ObservableCollection<Activity>(activities);
        }
    }
}