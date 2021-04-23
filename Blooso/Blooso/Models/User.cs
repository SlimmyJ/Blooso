namespace Blooso.Models
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public class User : ObservableObject
    {
        // [NotMapped]
        // public List<Tags> UserTags { get; set; } = new List<Tags>();

        // [NotMapped]
        // public List<Activities> ActivityList { get; set; } = new List<Activities>();
        private ICollection<User> _friendList = new List<User>();

        private ICollection<Message> _userFeedMessages = new List<Message>();

        public List<Activity> Activities { get; set; } = new List<Activity>();

        public DateTime DateOfBirth { get; set; }

        public ICollection<User> FriendList
        {
            get => this._friendList;
            set
            {
                this._friendList = value;
                this.OnPropertyChanged(nameof(this.FriendList));
            }
        }

        public int Id { get; set; }

        public bool IsInfected { get; set; }

        public bool IsVaccinated { get; set; }

        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                this.OnPropertyChanged(nameof(this.FriendList));
            }
        }

        public string Password { get; set; }

        public string Sex { get; set; }

        public string ShortBio { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<Message> UserFeedMessages
        {
            get => this._userFeedMessages;
            set
            {
                this._userFeedMessages = value;
                this.OnPropertyChanged(nameof(this.UserFeedMessages));
            }
        }

        public string UserLocation { get; set; }

        public string UserPicture { get; set; }

        private string _name { get; set; }

        public override string ToString()
        {
            var isInfected = this.IsInfected ? "infected" : "clean";
            var activities = this.Activities.Aggregate(string.Empty, (current, activity) => current + $"{activity}");
            var tags = this.Tags.Aggregate(string.Empty, (current, tag) => current + $"{tag}");
            var toString =
                $"{this.Name} {this.DateOfBirth} gender {this.Sex} {isInfected} {this.UserLocation} {activities} {tags}";

            return toString;
        }
    }
}