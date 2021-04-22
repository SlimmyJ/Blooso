using Blooso.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class EditActivityListViewModel : BaseViewModel
    {
        private ObservableCollection<Activities> _activities;

        public ObservableCollection<Activities> Activities
        {
            get => _activities;
            set
            {
                _activities = value;
                OnPropertyChanged(nameof(Activities));
            }
        }

        private ObservableCollection<Activities> _userActivities;

        public ObservableCollection<Activities> UserActivities
        {
            get => _userActivities;
            set
            {
                _userActivities = value;
                OnPropertyChanged(nameof(UserActivities));
            }
        }

        public Command<Activities> AddToActivityListCommand => new Command<Activities>(AddToActivityList);
        public Command<Activities> DeleteActivityFromListCommand => new Command<Activities>(DeleteActivityFromList);

        public EditActivityListViewModel()
        {
            Activities = new ObservableCollection<Activities>();
            UserActivities = new ObservableCollection<Activities>();
            _userRepo = UserRepository.GetRepository();
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
            GetUserActivities();
            GetAllActivities();
        }

        private void AddToActivityList(Activities activity)
        {
            if (UserActivities.Contains(activity)) Application.Current.MainPage.DisplayAlert("Glitch in the matrix", "You can only have one of each as your favorite activity", "OK");
            UserActivities.Add(activity);
        }

        private void DeleteActivityFromList(Activities activity)
        {
            if (UserActivities.Count != 1)
                UserActivities.Remove(activity);
            else
                Application.Current.MainPage.DisplayAlert("Hold up!", "You need at least one activity", "OK");
        }

        public void GetUserActivities()
        {
            var activities = CurrentUser.ActivityList;
            UserActivities = new ObservableCollection<Activities>(activities);
        }

        public void GetAllActivities()
        {
            var activities = Enum.GetValues(typeof(Activities)).Cast<Activities>().ToList();
            Activities = new ObservableCollection<Activities>(activities);
        }
    }
}