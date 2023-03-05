namespace BookReviews.Models
{
	public class Comment
	{
        public int CommentId { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public AppUser UserName { get; set; } = null!; // null forgiving operator
    }
}

