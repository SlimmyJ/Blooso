#region

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Blooso.Models
{
    public class Activity : ObservableObject
    {
        public Activity(int id, string name, int userId)
        {
            ActivityId = id;
            ActivityUserId = userId;
            Name = name;
        }

        public Activity()
        {
        }

        public Activity(int id, string name)
        {
            ActivityId = id;
            Name = name;
        }

        public int ActivityId { get; set; }

        public string Name { get; set; }

        public int ActivityUserId { get; set; }

        public User ActivityUser { get; set; }

        [NotMapped] public virtual ICollection<User> ActivityUsers { get; set; }
    }
}