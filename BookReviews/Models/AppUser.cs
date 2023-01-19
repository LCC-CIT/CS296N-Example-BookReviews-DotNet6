using Microsoft.AspNetCore.Identity;
using System;

namespace BookReviews.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime SignUpDate { get; set; }
    }
}

