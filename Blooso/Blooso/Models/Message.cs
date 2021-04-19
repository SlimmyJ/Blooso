using System;
using Xamarin.Forms;

namespace Blooso.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public User Author { get; set; }
        public User Recipient { get; set; }
        public string Text { get; set; }


    }
}