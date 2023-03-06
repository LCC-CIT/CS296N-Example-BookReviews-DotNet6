using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class Book
{
    // Backinig fields for properties

    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public ulong Isbn { get; set; }
    public string? Publisher { get; set; }

    [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
    public DateTime PubDate { get; set; }

    public List<Author> Authors { get; } = new();

    public List<Review> Reviews { get; } = new();
}