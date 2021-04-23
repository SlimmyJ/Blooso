using System;
using System.Collections.ObjectModel;
using System.Linq;
using Blooso.Models;
using Blooso.Repositories;
using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class EditActivityListViewModel : BaseViewModel
    {
        private ObservableCollection<Activity> _activities;

        public ObservableCollection<Activity> Activities
        {
            get => _activities;
            set
            {
                _activities = value;
                OnPropertyChanged(nameof(Activities));
            }
        }

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

        public EditActivityListViewModel()
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
            if (UserActivities.Contains(activity))
                Application.Current.MainPage.DisplayAlert("Glitch in the matrix",
                    "You can only have one of each as your favorite activity", "OK");
            UserActivities.Add(activity);
        }

        private void DeleteActivityFromList(Activity activity)
        {
            if (UserActivities.Count != 1)
                UserActivities.Remove(activity);
            else
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