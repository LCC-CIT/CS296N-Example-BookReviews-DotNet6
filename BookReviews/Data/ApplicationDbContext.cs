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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure cascade delete of Books when deleting an Author
        builder.Entity<Author>()
            .HasMany(a => a.Books)   // these two lines configure the many-to-many relationship
            .WithMany(b => b.Authors);
          //  .OnDelete(DeleteBehavior.Cascade);  // TODO: fix this bug
        // FYI EF would configure the liniking table for the many-to-many relationship without 
        // this code, but we needed to set the cascade delete behavior.
    }
}