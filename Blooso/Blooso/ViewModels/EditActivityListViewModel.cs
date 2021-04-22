using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Blooso.Interfaces;
using Blooso.Models;
using Blooso.Repositories;

namespace Blooso.ViewModels
{
    public class EditActivityListViewModel : BaseViewModel
    {
        private ObservableCollection<Activities> _activitieses;

        public ObservableCollection<Activities> Activities
        {
            get => _activitieses;
            set
            {
                _activitieses = value;
                OnPropertyChanged(nameof(Activities));
            }
        }

        private IUserRepository _userRepo;

        public EditActivityListViewModel()
        {
            Activities = new ObservableCollection<Activities>();
            _userRepo = UserRepository.GetRepository();
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
            GetUserActivities();
        }

        public void GetUserActivities()
        {
            var activities = CurrentUser.ActivityList;
            Activities = new ObservableCollection<Activities>(activities);
        }
    }
}