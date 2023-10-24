using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class RegisterVM
{
    [Required]
    [StringLength(255)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a password")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm your password")]
    [DataType(DataType.Password)]
    [Display(Name = "ConfirmPassword")]
    public string ConfirmPassword { get; set; } = string.Empty;
}