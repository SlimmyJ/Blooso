using System;
using System.Collections.Generic;

namespace Blooso.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public bool IsVaccinated { get; set; }

        public bool IsInfected { get; set; }

        public string UserPicture { get; set; }

        public string UserLocation { get; set; }

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

            var toString = $"{Name} {DateOfBirth} gender {Sex} {isInfected} {UserLocation} {activities} {tags}";

            return toString;
        }
    }
}