namespace Blooso.Models
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public class User : ObservableObject
    {
        private ICollection<Message> _feedMessages = new List<Message>();

        private ICollection<User> _friendList = new List<User>();

        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

        public DateTime DateOfBirth { get; set; }

        public ICollection<User> FriendList
        {
            get => _friendList;
            set
            {
                _friendList = value;
                OnPropertyChanged(nameof(FriendList));
            }
        }

        public int Id { get; set; }

        public bool IsInfected { get; set; }

        public bool IsVaccinated { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(FriendList));
            }
        }

        public string Password { get; set; }

        public string Sex { get; set; }

        public string ShortBio { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<Message> UserFeedMessages
        {
            get => _feedMessages;
            set
            {
                _feedMessages = value;
                OnPropertyChanged(nameof(UserFeedMessages));
            }
        }

        public string UserLocation { get; set; }

        public string UserPicture { get; set; }

        private string _name { get; set; }

        public override string ToString()
        {
            string isInfected = IsInfected ? "infected" : "clean";
            string activities = Activities.Aggregate(string.Empty, (current, activity) => current + $"{activity}");
            string tags = Tags.Aggregate(string.Empty, (current, tag) => current + $"{tag}");
            var toString =
                $"{Name} {DateOfBirth} gender {Sex} {isInfected} {UserLocation} {activities} {tags}";

            return toString;
        }
    }
}