using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blooso.Models
{
    public class User : ObservableObject
    {
        public int Id { get; set; }

        private string _name { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(FriendList));
            }
        }

        public char Sex { get; set; }
        public bool IsVaccinated { get; set; }

        public bool IsInfected { get; set; }

        public string UserPicture { get; set; }

        public string UserLocation { get; set; }

        public DateTime DateOfBirth { get; set; }
        public List<Tags> UserTags { get; set; }

        public List<Activities> ActivityList { get; set; }

        private ObservableCollection<User> _friendList;
        private ObservableCollection<Message> _userFeedMessages;

        public ObservableCollection<User> FriendList
        {
            get { return _friendList; }
            set
            {
                _friendList = value;
                OnPropertyChanged(nameof(FriendList));
            }
        }

        public ObservableCollection<Message> UserFeedMessages
        {
            get { return _userFeedMessages; }
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