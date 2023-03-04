using System;

namespace BookReviews.Models
{
     public class Review
    {
        public int ReviewId { get; set; }
        public AppUser Reviewer { get; set; } = null!;  // null forgiving operator, !, supresses warnings
        public string ReviewText { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
        public List<Comment> Comments { get; } = new List<Comment>();

        public int BookId { get; set; }  // Foreign key to enable EF to do cascade delete
        
    }
}
