﻿namespace Blooso.ViewModels
{
    #region

    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Blooso.Models;

    using Xamarin.Forms;

    #endregion

    public class EditActivityListViewModel : BaseViewModel
    {
        private ObservableCollection<Activity> _userActivities;

        public ObservableCollection<Activity> UserActivities
        {
            get => _userActivities;
            set
            {
                _userActivities = value;
                OnPropertyChanged(nameof(UserActivities));
            }
        }

        public Command<Activity> AddToActivityListCommand => new Command<Activity>(AddToActivityList);

        public Command<Activity> DeleteActivityFromListCommand => new Command<Activity>(DeleteActivityFromList);

        private void AddToActivityList(Activity activity)
        {
            if (UserActivities.Contains(activity))
            {
                Application.Current.MainPage.DisplayAlert(
                    "Glitch in the matrix",
                    "You can only have one of each as your favorite activity",
                    "OK");
            }

            UserActivities.Add(activity);
        }

        private void DeleteActivityFromList(Activity activity)
        {
            if (UserActivities.Count != 1)
            {
                UserActivities.Remove(activity);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Hold up!", "You need at least one activity", "OK");
            }
        }

        public void GetUserActivities()
        {
            ICollection<Activity> activities = CurrentUser.Activities;
            UserActivities = new ObservableCollection<Activity>(activities);
        }
    }
}