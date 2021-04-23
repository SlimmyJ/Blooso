using System;
using System.Collections.ObjectModel;
using System.Linq;
using Blooso.Models;
using Blooso.Repositories;
using Xamarin.Forms;

    #endregion

    public class EditActivityListViewModel : BaseViewModel
    {
        private ObservableCollection<Activities> _activities;

        private ObservableCollection<Activity> _userActivities;

        public EditActivityListViewModel()
        {
            this.Activities = new ObservableCollection<Activities>();
            this.UserActivities = new ObservableCollection<Activities>();
            this._userRepo = UserRepository.GetRepository();
            this.CurrentUser = this._userRepo.GetCurrentlyLoggedInUser();
            this.GetUserActivities();
            this.GetAllActivities();
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

        public Command<Activities> AddToActivityListCommand => new Command<Activities>(this.AddToActivityList);

        public Command<Activities> DeleteActivityFromListCommand =>
            new Command<Activities>(this.DeleteActivityFromList);

        public ObservableCollection<Activities> UserActivities
        {
            get => this._userActivities;
            set
            {
                this._userActivities = value;
                this.OnPropertyChanged(nameof(this.UserActivities));
            }
        }

        public void GetAllActivities()
        {
            var activities = Enum.GetValues(typeof(Activities)).Cast<Activities>().ToList();
            this.Activities = new ObservableCollection<Activities>(activities);
        }

        public void GetUserActivities()
        {
            List<Activity> activities = this.CurrentUser.Activities;
            this._userActivities = new ObservableCollection<Activity>(activities);
        }

        private void AddToActivityList(Activities activity)
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

        private void DeleteActivityFromList(Activities activity)
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
            var activities = Enum.GetValues(typeof(Activities)).Cast<Activities>().ToList();
            Activities = new ObservableCollection<Activities>(activities);
        }
    }
}