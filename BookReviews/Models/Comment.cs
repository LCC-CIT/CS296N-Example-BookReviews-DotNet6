using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class Comment
{
    public int CommentId { get; set; }
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string CommentText { get; set; } = String.Empty;
    public AppUser Commenter { get; set; }
    public int ReviewId { get; set; } // Composition (a comment is part of a review.)
}