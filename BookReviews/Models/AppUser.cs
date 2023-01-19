using System;

namespace BookReviews.Models
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        public string UserName { get; set; }
        public DateTime SignUpDate { get; set; }
    }
}

