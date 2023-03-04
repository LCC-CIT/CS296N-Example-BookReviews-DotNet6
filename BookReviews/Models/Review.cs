using System;

namespace BookReviews.Models
{
    public class Review
    {
        private List<Comment> comments = new();

        public int ReviewId { get; set; }
        public AppUser Reviewer { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public ICollection<Comment> Comments { get => comments; }  
    }
}
