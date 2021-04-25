namespace Blooso.ViewModels
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Models;

    using Repositories;

    using Xamarin.Forms;

    #endregion

    public class EditUserTagsListViewModel : BaseViewModel
    {
        private ICollection<Tag> _tags;

        private ICollection<Tag> _userTags;

        public EditUserTagsListViewModel()
        {
            Tags = new ObservableCollection<Tag>();
            UserTags = new ObservableCollection<Tag>();
            _userRepo = UserRepository.GetRepository();
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
            GetUserTags();
            GetAllTags();
        }

        public Command<Tag> AddToTagsListCommand => new(tag => AddToTagsList(tag));

        public Command<Tag> DeleteTagFromListCommand => new(tag => DeleteTagFromList(tag));

        public ObservableCollection<Tag> Tags
        {
            get => _tags as ObservableCollection<Tag>;
            set
            {
                _tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }

        public ObservableCollection<Tag> UserTags
        {
            get => _userTags as ObservableCollection<Tag>;
            set
            {
                _userTags = value;
                OnPropertyChanged(nameof(UserTags));
            }
        }

        public void GetAllTags()
        {
            var tags = Enum.GetValues(typeof(Tag)).Cast<Tag>().ToList();
            Tags = new ObservableCollection<Tag>(tags);
        }

        public void GetUserTags()
        {
            ICollection<Tag> tags = CurrentUser.Tags;
            UserTags = new ObservableCollection<Tag>(tags);
        }

        private void AddToTagsList(Tags tag)
        {
            if (UserTags.Contains(tag))
                Application.Current.MainPage.DisplayAlert("Glitch in the matrix",
                    "You can only have one of each as your favorite tag", "OK");
        private void AddToTagsList(Tag tag)
        {
            if (UserTags.Contains(tag))
            {
                Application.Current.MainPage.DisplayAlert(
                    "Glitch in the matrix",
                    "You can only have one of each as your favorite tag",
                    "OK");
            }

            UserTags.Add(tag);
        }

        private void DeleteTagFromList(Tag tag)
        {
            if (UserTags.Count != 1)
            {
                UserTags.Remove(tag);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Hold up!", "You need at least one tag", "OK");
            }
        }
    }
}