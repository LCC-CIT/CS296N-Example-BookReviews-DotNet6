using BookReviews.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookReviews;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Review> Reviews { get; set; }

    // Note: Cascade delete can't be configured for many-to-many relationships in OnModelCreating
    // Cascade deletes for many-to-many entities needs to be done explicitly in our own code.
    // 

}