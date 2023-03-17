namespace BookReviews.Models;

public class CommentVM
{
    public int ReviewId { get; set; } // This identifies the review being commented on
    public int BookId { get; set; }
    public string CommentText { get; set; } = String.Empty;
}