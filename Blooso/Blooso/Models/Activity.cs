using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blooso.Models
{
    public class Activity : ObservableObject
    {
        public Activity(int id, string name)
        {
            ActivityId = id;
            Name = name;
        }

        public int ActivityId { get; set; }

        public string Name { get; set; }

        [ForeignKey("UserId")] public int ActivityUserId { get; set; }

        public User ActivityUser { get; set; }

        [NotMapped] public virtual ICollection<User> ActivityUsers { get; set; }
    }
}