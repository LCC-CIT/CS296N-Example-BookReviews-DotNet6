using BookReviews.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReviews
{
    public class ApplicationDbContext : DbContext   
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
