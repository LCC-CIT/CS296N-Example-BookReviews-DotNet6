using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class Book
{
    public int BookId { get; set; }
    public string BookTitle { get; set; } = String.Empty;
    public ulong Isbn { get; set; }
    public string? Publisher { get; set; }

    [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
    public DateTime PubDate { get; set; }

    public List<Author> Authors { get; } = new();  // one side of a many-to-many relationship
    public List<Review> Reviews { get; } = new();

    // No nullable Author FK needed to prevent cascade deletes since Book and Author are in a many-to-many relationship.

}