using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookReviews.Models
{
    public class AppUser : IdentityUser
    {
        public DateOnly SignUpDate { get; set; }
        public string Name { get; set; } = string.Empty;

        [NotMapped] 
        public IList<string> RoleNames { get; set; } = null!;
    }
}

