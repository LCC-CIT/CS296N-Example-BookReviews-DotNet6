namespace BookReviews.Models;

public class Comment
{
    public int CommentId { get; set; }
    public string CommentText { get; set; } = String.Empty;
    public AppUser Commenter { get; set; } = new();
    public int ReviewId { get; set; } // Composition (a comment is part of a review.)
}