namespace Blooso.ViewModels
{
    #region

    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Blooso.Models;
    using Blooso.Repositories;

    using Xamarin.Forms;

    #endregion

    public class EditUserTagsListViewModel : BaseViewModel
    {
        private ObservableCollection<Tag> _tags;

        private ObservableCollection<Tag> _userTags;

        public EditUserTagsListViewModel()
        {
            this.Tags = new ObservableCollection<Tag>();
            this.UserTags = new ObservableCollection<Tag>();
            this._userRepo = UserRepository.GetRepository();
            this.CurrentUser = this._userRepo.GetCurrentlyLoggedInUser();
            this.GetUserTags();
            this.GetAllTags();
        }

        public Command<Tag> AddToTagsListCommand => new Command<Tag>(this.AddToTagsList);

        public Command<Tag> DeleteTagFromListCommand => new Command<Tag>(this.DeleteTagFromList);

        public ObservableCollection<Tag> Tags
        {
            get => this._tags;
            set
            {
                this._tags = value;
                this.OnPropertyChanged(nameof(this.Tags));
            }
        }

        public ObservableCollection<Tag> UserTags
        {
            get => this._userTags;
            set
            {
                this._userTags = value;
                this.OnPropertyChanged(nameof(this.UserTags));
            }
        }

        public void GetAllTags()
        {
            var tags = Enum.GetValues(typeof(Tag)).Cast<Tag>().ToList();
            this.Tags = new ObservableCollection<Tag>(tags);
        }

        public void GetUserTags()
        {
            var tags = CurrentUser.Tags;
            UserTags = new ObservableCollection<Tag>(tags);
        }

        private void AddToTagsList(Tag tag)
        {
            if (this.UserTags.Contains(tag))
            {
                Application.Current.MainPage.DisplayAlert(
                    "Glitch in the matrix",
                    "You can only have one of each as your favorite tag",
                    "OK");
            }

            this.UserTags.Add(tag);
        }

        private void DeleteTagFromList(Tag tag)
        {
            if (this.UserTags.Count != 1)
            {
                this.UserTags.Remove(tag);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Hold up!", "You need at least one tag", "OK");
            }
        }
    }
}