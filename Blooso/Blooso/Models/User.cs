using System;
using System.Collections.Generic;
using System.Linq;

namespace Blooso.Models
{
    public class User
    {
        public int Id { get; set; }

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

        public string ShortBio { get; set; }

        public override string ToString()
        {
            string isInfected;
            isInfected = IsInfected ? "infected" : "clean";

            var activities = ActivityList.Aggregate("", (current, activity) => current + $"{activity}");
            var tags = UserTags.Aggregate("", (current, tag) => current + $"{tag}");

            var toString =
                $"{Name} {DateOfBirth} gender {Sex} {isInfected} {UserLocation.AreaCode} {activities} {tags}";

            return toString;
        }
    }
}