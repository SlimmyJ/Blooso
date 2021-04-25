using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blooso.Models
{
    #region

    #endregion

    public class Tag : ObservableObject
    {
        public Tag(int tagId, string tagName)
        {
            TagId = tagId;
            TagName = tagName;
        }

        [Key] public int TagId { get; set; }

        [ForeignKey("UserId")] private int TagUserId { get; set; }

        public string TagName { get; set; }

        [Key] public User TagUser { get; set; }

        [NotMapped] public virtual ICollection<User> TagUsers { get; set; }
    }
}