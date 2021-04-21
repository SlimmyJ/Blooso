using System;

namespace Blooso.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public User Author { get; set; }
        public User Recipient { get; set; }
        public bool Private { get; set; }
        public bool IsPositiveReview { get; set; }
        public string Text { get; set; }
    }
}