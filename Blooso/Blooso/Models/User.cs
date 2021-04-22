namespace Blooso.Models
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    #endregion

    public class User : ObservableObject
    {
        private ObservableCollection<User> _friendList;

        private ObservableCollection<Message> _userFeedMessages;

        public List<Activities> ActivityList { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ObservableCollection<User> FriendList
        {
            get
            {
                return this._friendList;
            }

            set
            {
                this._friendList = value;
                this.OnPropertyChanged(nameof(this.FriendList));
            }
        }

        public int Id { get; set; }

        public bool IsInfected { get; set; }

        public bool IsVaccinated { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public string ShortBio { get; set; }

        public ObservableCollection<Message> UserFeedMessages
        {
            get
            {
                return this._userFeedMessages;
            }

            set
            {
                this._userFeedMessages = value;
                this.OnPropertyChanged(nameof(this.UserFeedMessages));
            }
        }

        public string UserLocation { get; set; }

        public string UserPicture { get; set; }

        public List<Tags> UserTags { get; set; }

        public override string ToString()
        {
            var isInfected = this.IsInfected ? "infected" : "clean";
            var activities = this.ActivityList.Aggregate(string.Empty, (current, activity) => current + $"{activity}");
            var tags = this.UserTags.Aggregate(string.Empty, (current, tag) => current + $"{tag}");
            var toString =
                $"{this.Name} {this.DateOfBirth} gender {this.Sex} {isInfected} {this.UserLocation} {activities} {tags}";

            return toString;
        }
    }
}