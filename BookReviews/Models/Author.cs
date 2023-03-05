using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models
{
	public class Author
	{
		public int AuthorId { get; set; }
		public string Name { get; set; } = string.Empty;

		[DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]

	public DateOnly Birthdate { get; set; }
        public List<Book> Books { get; } = new List<Book>();  // Books written by this author

        // FK for list of Authors on the Book class
        public int? BookId { get; set; } // Nullable FK. No cascade delete of Author with Book.
    }
}

