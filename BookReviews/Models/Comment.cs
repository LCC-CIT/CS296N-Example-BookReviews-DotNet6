using System;
namespace BookReviews.Models
{
	public class Comment
	{
        // Comment has a shadow FK, composition relationship to Review, will be cascade deleted
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public AppUser UserName { get; set; }
    }
}

