using BookReviews.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReviews.Data;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public IQueryable<Book> Books
    {
        get
        {
            // Get all the Book objects in the Books DbSet
            // and include the related data objects.
            return _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.Reviewer)
                // Include Reviews again so that we can also include comments (Reviews won't be duplicated)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.Comments)
                .ThenInclude(c => c.Commenter);
        }
    }

    public IQueryable<Review> Reviews
    {
        get
        {
            return _context.Reviews
                .Include(r => r.Reviewer)
                .Include(r => r.Comments)
                .ThenInclude(c => c.Commenter);
        }
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        // Get the book with the specified id
        return await Books.Where(b => b.BookId == id).FirstOrDefaultAsync();
    }

    public async Task<Review?> GetReviewByIdAsync(int id)
    {
        // Get the review with the specified id
        return await Reviews.Where(r => r.ReviewId == id)
                .FirstOrDefaultAsync();
    }

    // Update a book if it already exists, add it if it doesn't
    public async Task<int> AddOrUpdateBookAsync(Book model)
    {
        _context.Books.Update(model);
        var result = await _context.SaveChangesAsync();
        return result;
        // returns a positive value if successful
    }
}