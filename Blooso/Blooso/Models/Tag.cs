﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blooso.Models
{
    #region

    #endregion

    public class Tag : ObservableObject
    {
        public Tag(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> TagUsers { get; set; }
    }
}