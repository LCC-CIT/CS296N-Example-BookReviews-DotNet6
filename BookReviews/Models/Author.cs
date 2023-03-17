using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class Author
{
    public int AuthorId { get; set; }

    public string Name { get; set; } = String.Empty;

    [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Birthdate { get; set; }

    public List<Book> Books { get; } = new();   // one side of a many-to-many relationship.

    // No nullable Book FK needed to prevent cascade deletes since Book and Author are in a many-to-many relationship.
}