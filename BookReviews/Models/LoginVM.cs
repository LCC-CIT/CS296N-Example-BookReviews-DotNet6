using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class LoginVM
{
    [Required]
    [StringLength(255)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Password { get; set; } = string.Empty;

    public string ReturnUrl { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}