using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int TagUserId { get; set; }

        public string Name { get; set; }

        [ForeignKey("TagUserId")] public virtual ICollection<User> TagUsers { get; set; }
    }
}