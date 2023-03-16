using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class Book
{
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public ulong Isbn { get; set; }
    public string? Publisher { get; set; }

    [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
    public DateTime PubDate { get; set; }
    public List<Author> Authors { get; } = new();  // one side of a many-to-many relationship
    public List<Review> Reviews { get; } = new();
    public int AuthorId { get; set; } // Do cascade delete books with their author(s).
}