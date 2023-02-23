using System;

namespace BookReviews.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public AppUser Reviewer { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public ICollection<Comment> Comments { get; set; }  // Compositioin--FK in comment
        public int BookId { get; set; }      // Composition (a review is part of a book.)
    }
}
