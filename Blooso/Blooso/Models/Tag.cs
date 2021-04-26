#region

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

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

        public Tag()
        {
        }

        public int TagId { get; set; }

        private int TagUserId { get; set; }

        public string TagName { get; set; }

        public User TagUser { get; set; }

        [NotMapped] public virtual ICollection<User> TagUsers { get; set; }
    }
}