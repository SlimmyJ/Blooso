using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public int UserId { get; set; }

        public ObservableCollection<User> Users { get; set; }
    }
}