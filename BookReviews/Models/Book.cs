namespace BookReviews.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public ulong Isbn { get; set; }
        public string? Publisher { get; set; }
        public DateOnly PubDate { get; set; }
        public List<Author> Authors { get; } = new List<Author>();
        public List<Review> Reviews { get; } = new List<Review>();

        // FK for list of Books on the Author class
        public int? AuthorId { get; set; } // nullable FK to disable cascascade delete of Book with Author
    }
}
