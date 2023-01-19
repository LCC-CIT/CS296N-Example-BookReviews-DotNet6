using System;

namespace BookReviews.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public Book Book { get; set; }
        public AppUser Reviewer { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
