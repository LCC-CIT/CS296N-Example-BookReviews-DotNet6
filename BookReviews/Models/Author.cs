using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; }

    [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Birthdate { get; set; }

    public int? BookId { get; set; } // Nullable- no cascade delete.
}