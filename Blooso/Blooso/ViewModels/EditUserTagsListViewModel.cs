using Blooso.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Blooso.ViewModels
{
    public class EditUserTagsListViewModel : BaseViewModel
    {
        private ObservableCollection<Tags> _tags;

        public ObservableCollection<Tags> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }

        private ObservableCollection<Tags> _userTags;

        public ObservableCollection<Tags> UserTags
        {
            get => _userTags;
            set
            {
                _userTags = value;
                OnPropertyChanged(nameof(UserTags));
            }
        }

        public EditUserTagsListViewModel()
        {
            Tags = new ObservableCollection<Tags>();
            UserTags = new ObservableCollection<Tags>();
            _userRepo = UserRepository.GetRepository();
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
            GetUserTags();
            GetAllTags();
        }

        public Command<Tags> AddToTagsListCommand => new Command<Tags>(AddToTagsList);
        public Command<Tags> DeleteTagFromListCommand => new Command<Tags>(DeleteTagFromList);

        private void AddToTagsList(Tags tag)
        {
            if (UserTags.Contains(tag)) Application.Current.MainPage.DisplayAlert("Glitch in the matrix", "You can only have one of each as your favorite tag", "OK");
            UserTags.Add(tag);
        }

        private void DeleteTagFromList(Tags tag)
        {
            if (UserTags.Count != 1)
                UserTags.Remove(tag);
            else
                Application.Current.MainPage.DisplayAlert("Hold up!", "You need at least one tag", "OK");
        }

        public void GetUserTags()
        {
            var tags = CurrentUser.UserTags;
            UserTags = new ObservableCollection<Tags>(tags);
        }

        public void GetAllTags()
        {
            var tags = Enum.GetValues(typeof(Tags)).Cast<Tags>().ToList();
            Tags = new ObservableCollection<Tags>(tags);
        }
    }
}