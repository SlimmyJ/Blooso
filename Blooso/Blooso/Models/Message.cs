﻿using System;

namespace Blooso.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsPositiveReview { get; set; }
        public string Text { get; set; }

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public int RecipientId { get; set; }
        public virtual User Recipient { get; set; }
    }
}