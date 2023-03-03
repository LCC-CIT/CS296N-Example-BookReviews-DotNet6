using System;

namespace BookReviews.Models
{
    public class Book
    {
        private List<Author> authorList = new List<Author>();
        private List<Review> reviewList = new List<Review>();

        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Isbn { get; set; }
        public string? Publisher { get; set; }
        public DateTime PubDate { get; set; }

        public List<Author> Authors     // See nullable FK in Author to prevent cascade delete
        { get { return authorList; } }

        public List<Review> Reviews     // Will be cascade deleted by default
        { get { return reviewList; } }
    }
}
