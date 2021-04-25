using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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

        [Key] public int ActivityId { get; set; }

        [Key] public string Name { get; set; }

        [ForeignKey("UserId")] public int ActivityUserId { get; set; }

        [Key] public User ActivityUser { get; set; }

        [NotMapped] public virtual ICollection<User> ActivityUsers { get; set; }
    }
}