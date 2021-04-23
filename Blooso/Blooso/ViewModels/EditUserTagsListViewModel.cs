namespace Blooso.ViewModels
{
    #region

    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Blooso.Repositories;

    using Xamarin.Forms;

    #endregion

    public class EditUserTagsListViewModel : BaseViewModel
    {
        private ObservableCollection<Tags> _tags;

        private ObservableCollection<Tags> _userTags;

        public EditUserTagsListViewModel()
        {
            this.Tags = new ObservableCollection<Tags>();
            this.UserTags = new ObservableCollection<Tags>();
            this._userRepo = UserRepository.GetRepository();
            this.CurrentUser = this._userRepo.GetCurrentlyLoggedInUser();
            this.GetUserTags();
            this.GetAllTags();
        }

        public Command<Tags> AddToTagsListCommand => new Command<Tags>(this.AddToTagsList);

        public Command<Tags> DeleteTagFromListCommand => new Command<Tags>(this.DeleteTagFromList);

        public ObservableCollection<Tags> Tags
        {
            get => this._tags;
            set
            {
                this._tags = value;
                this.OnPropertyChanged(nameof(this.Tags));
            }
        }

        public ObservableCollection<Tags> UserTags
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
            var tags = Enum.GetValues(typeof(Tags)).Cast<Tags>().ToList();
            this.Tags = new ObservableCollection<Tags>(tags);
        }

        public void GetUserTags()
        {
            var tags = this.CurrentUser.Tags;
            var tagList = tags.Select(tag => tag.Id).ToList();

            this.UserTags = new ObservableCollection<Tags>(tagList);
        }

        private void AddToTagsList(Tags tag)
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

        private void DeleteTagFromList(Tags tag)
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