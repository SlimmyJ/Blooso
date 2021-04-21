using System;
using System.Collections.Generic;

using Blooso;

namespace Blooso.Models
{
    public class User
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name { get; set; }
        public char Sex { get; set; }
        public bool IsVaccinated { get; set; }

        public bool IsInfected { get; set; }

        public string UserPicture { get; set; }

        public UserLocation UserLocation { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<Activities> ActivityList { get; set; }

        public List<User> FriendsList { get; set; }

        public List<Tags> UserTags { get; set; }

        public List<Message> ProfileCommentsList { get; set; }

        //public string ShortBio { get; set; } 
      
        public override string ToString()
        {
            var isInfected = IsInfected ? "infected" : "clean";

            var activities = "";
            foreach (var activity in ActivityList)
            {
                activities += $"{activity }";
            }

            var tags = "";
            foreach (var tag in UserTags)
            {
                tags += $"{tag }";
            }

            var toString = $"{Name} {DateOfBirth} {Sex} {isInfected} {UserLocation.AreaCode} {activities} {tags}";

            return toString;
        }
    }
}