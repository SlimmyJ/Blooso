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
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        [ForeignKey("Id")] public virtual ICollection<User> ActivityUsers { get; set; }
    }
}