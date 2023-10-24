using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class Author
{
    public int AuthorId { get; set; }

    [Required]
    public string Name { get; set; }

    [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Birthdate { get; set; }

    public List<Book> Books { get; } = new();

    // public int? BookId { get; set; } // Nullable--no cascade delete with book.
}