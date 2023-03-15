using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class Book
{
    // Backinig fields for properties

    public int BookId { get; set; }

    [Required]
    [StringLength(100)]
    public string BookTitle { get; set; }

    [Range(1000000000, 9999999999999)]  // 10 or 13 digit ISBN without dashes
    public ulong Isbn { get; set; }

    public string? Publisher { get; set; }

    // [Range(typeof(DateTime), "01/01/1450", "12/31/2099", ErrorMessage = "Date for {0} must be between {1} and {2}")]
    [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
     public DateTime PubDate { get; set; }

    public List<Author> Authors { get; } = new();

    public List<Review> Reviews { get; } = new();
}