using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BookReviews.Models;

public class AppUser : IdentityUser
{
    public DateTime SignUpDate { get; set; }
    public string Name { get; set; }

    [NotMapped] public IList<string> RoleNames { get; set; } = null!;
}