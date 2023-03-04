using System;

namespace BookReviews.Models
{
     public class Review
    {

     
        private List<Comment> comments = new();  // Backing field for Comments

        public int ReviewId { get; set; }
        public AppUser Reviewer { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public ICollection<Comment> Comments { get => comments; } 
        public int BookId { get; set; }  // Foreign key to enable EF to do cascade delete
        
    }
}
