using System;

namespace BookReviews.Models
{
     public class Review
    {
        // Review has a shadow non-nullable FK so this will be cascade deleted
     
        private List<Comment> comments = new();  // Backing field for Comments

        public int ReviewId { get; set; }
        public AppUser Reviewer { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public ICollection<Comment> Comments { get => comments; }  // Will be cascade deleted-shadow non-nullable FK in Comment
        
    }
}
