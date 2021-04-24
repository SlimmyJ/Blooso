using System.Collections.Generic;

namespace Blooso.Models
{
    #region

    #endregion

    public class Activity : ObservableObject
    {
        public Activity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}