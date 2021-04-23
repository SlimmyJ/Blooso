using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Blooso.Models
{
    public class User : ObservableObject
    {
        public int Id { get; set; }

        private string _name { get; set; }

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

        public bool IsVaccinated { get; set; }

        public bool IsInfected { get; set; }

        public string UserPicture { get; set; }

        public string UserLocation { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public List<Activity> Activities { get; set; } = new List<Activity>();

        [NotMapped]
        public List<Tags> UserTags { get; set; } = new List<Tags>();

        [NotMapped]
        public List<Activities> ActivityList { get; set; } = new List<Activities>();

        private ICollection<User> _friendList = new List<User>();

        public ICollection<User> FriendList
        {
            get => _friendList;
            set
            {
                _friendList = value;
                OnPropertyChanged(nameof(FriendList));
            }
        }

        private ICollection<Message> _userFeedMessages = new List<Message>();

        public ICollection<Message> UserFeedMessages
        {
            get => _userFeedMessages;
            set
            {
                _userFeedMessages = value;
                OnPropertyChanged(nameof(UserFeedMessages));
            }
        }

        public string ShortBio { get; set; }

        public override string ToString()
        {
            var isInfected = IsInfected ? "infected" : "clean";
            var activities = ActivityList.Aggregate("", (current, activity) => current + $"{activity}");
            var tags = UserTags.Aggregate("", (current, tag) => current + $"{tag}");
            var toString = $"{Name} {DateOfBirth} gender {Sex} {isInfected} {UserLocation} {activities} {tags}";

            return toString;
        }
    }
}