using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; }

    [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Birthdate { get; set; }
    public List<Book> Books { get; } = new();   // one side of a many-to-many relationship

    public int? BookId { get; set; } // Nullable--no cascade delete of author with Book.
}