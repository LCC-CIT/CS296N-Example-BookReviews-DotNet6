using System.ComponentModel.DataAnnotations;

namespace BookReviews.Models;

public class Review
{
    private readonly List<Comment> comments = new(); // Backing field for Comments

    public int ReviewId { get; set; }
    public AppUser Reviewer { get; set; }
    public string ReviewText { get; set; }

    [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ReviewDate { get; set; }

    public ICollection<Comment> Comments => comments;
    public int BookId { get; set; } // Foreign key to enable EF to do cascade delete
}