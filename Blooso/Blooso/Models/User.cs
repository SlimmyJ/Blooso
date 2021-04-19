using System;
using System.Collections.Generic;

namespace Blooso.Models
{
    public class User
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name { get; set; }
        public char Sex { get; set; }
        public bool IsVaccinated { get; set; }

        public bool IsInfected { get; set; }

        public UserLocation UserLocation { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<Activities> ActivityList { get; set; }

        public List<User> FriendsList { get; set; }

        public List<Tags> UserTags { get; set; }

        public List<Message> Messages { get; set; }

        public List<User> UserLikes { get; set; }
        public List<User> LikedByUser { get; set; }
    }
}