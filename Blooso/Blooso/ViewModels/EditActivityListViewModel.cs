#region

#endregion

namespace Blooso.ViewModels
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
            get => this._userActivities;
            set
            {
                this._userActivities = value;
                this.OnPropertyChanged(nameof(this.UserActivities));
            }
        }

        public Command<Activity> AddToActivityListCommand => new Command<Activity>(this.AddToActivityList);

        public Command<Activity> DeleteActivityFromListCommand => new Command<Activity>(this.DeleteActivityFromList);

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
        }

        public void GetUserActivities()
        {
            ICollection<Activity> activities = this.CurrentUser.Activities;
            this.UserActivities = new ObservableCollection<Activity>(activities);
        }
    }
}