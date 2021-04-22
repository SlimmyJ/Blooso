namespace Blooso.Models
{
    #region

    using System;

    #endregion

    public class Message
    {
        public User Author { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Id { get; set; }

        public bool IsPositiveReview { get; set; }

        public bool Private { get; set; }

        public User Recipient { get; set; }

        public string Text { get; set; }
    }
}