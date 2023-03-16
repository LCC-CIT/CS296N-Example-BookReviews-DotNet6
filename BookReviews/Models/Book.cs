using System;

namespace BookReviews.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public int? Isbn { get; set; }
        public string? Publisher { get; set; }
        public DateTime PubDate { get; set; }
    }
}
