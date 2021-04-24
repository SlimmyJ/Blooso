﻿namespace Blooso.Models
{
    #region

    using System.Collections.Generic;

    #endregion

    public class Tag : ObservableObject
    {
        public Tag(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}