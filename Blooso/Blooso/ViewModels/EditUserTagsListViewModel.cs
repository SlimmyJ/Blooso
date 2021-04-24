﻿namespace Blooso.ViewModels
{
    #region

    using System;
    using System.Collections.Generic;
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
            Tags = new ObservableCollection<Tag>();
            UserTags = new ObservableCollection<Tag>();
            _userRepo = UserRepository.GetRepository();
            CurrentUser = _userRepo.GetCurrentlyLoggedInUser();
            GetUserTags();
            GetAllTags();
        }

        public Command<Tag> AddToTagsListCommand => new Command<Tag>(tag => AddToTagsList(tag));

        public Command<Tag> DeleteTagFromListCommand => new Command<Tag>(tag => DeleteTagFromList(tag));

        public ObservableCollection<Tag> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }

        public ObservableCollection<Tag> UserTags
        {
            get => _userTags;
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