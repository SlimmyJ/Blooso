#region

using System.Collections.Generic;
using System.Collections.ObjectModel;

using Blooso.Data;
using Blooso.Models;

using Xamarin.Forms;

#endregion

namespace Blooso.ViewModels
{
    #region

    #endregion

    public class EditActivityListViewModel : BaseViewModel
    {
        private ICollection<Activity> _userActivities;

        public EditActivityListViewModel(DummyData fakerBroDummyData, ICollection<Activity> userActivities) : base(
            fakerBroDummyData) =>
            _userActivities = userActivities;

        public EditActivityListViewModel() : base(DummyData.Instance)
        {
        }

        public ObservableCollection<Activity> UserActivities
        {
            get => _userActivities as ObservableCollection<Activity>;
            set
            {
                _userActivities = value;
                OnPropertyChanged(nameof(UserActivities));
            }
        }

        public Command<Activity> AddToActivityListCommand => new(AddToActivityList);

        public Command<Activity> DeleteActivityFromListCommand => new(DeleteActivityFromList);

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
            IEnumerable<Activity> activities = CurrentUser.Activities;
            UserActivities = new ObservableCollection<Activity>(activities);
        }
    }
}