#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Blooso.Data;
using Blooso.Models;

using Xamarin.Forms;

#endregion

namespace Blooso.ViewModels
{
    #region

    #endregion

    public class EditUserTagsListViewModel : BaseViewModel
    {
        private ICollection<Tag> _tags;
        private ICollection<Tag> _userTags;

        public EditUserTagsListViewModel() : base(DummyData.Instance)
        {
            Tags = new ObservableCollection<Tag>();
            UserTags = new ObservableCollection<Tag>();
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
            GetUserTags();
            GetAllTags();
        }

        public Command<Tag> AddToTagsListCommand => new(tag => AddToTagsList(tag));

        public Command<Tag> DeleteTagFromListCommand => new(tag => AddToTagsList(tag));

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

        private void AddToTagsList(Tag tag)
        {
            if (UserTags.Contains(tag))
            {
                Application.Current.MainPage.DisplayAlert(
                    "Glitch in the matrix",
                    "You can only have one of each as your favorite tag",
                    "OK");
            }

            void ThisAddToTagsList(Tag thisTag)
            {
                if (UserTags.Contains(thisTag))
                {
                    Application.Current.MainPage.DisplayAlert(
                        "Glitch in the matrix",
                        "You can only have one of each as your favorite tag",
                        "OK");
                }

                UserTags.Add(thisTag);
            }

            void ThisDeleteTagFromList(Tag todeleteTag)
            {
                if (UserTags.Count != 1)
                {
                    UserTags.Remove(todeleteTag);
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Hold up!", "You need at least one tag", "OK");
                }
            }
        }
    }
}