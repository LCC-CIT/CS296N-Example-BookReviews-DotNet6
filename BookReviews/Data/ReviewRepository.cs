using BookReviews.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReviews.Data
{
    public class ReviewRepository : IReviewRepository
    {
        private ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IQueryable<Book> Books
        {
            get
            {
                // Get all the Book objects in the Books DbSet
                // and include the related data objects.
                return _context.Books.Include(b => b.Authors)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.Reviewer)
                // Include Reviews again so that we can also include comemnts (Reviews won't be duplicated)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.Comments);
            }
        }

        public IQueryable<Review> Reviews => throw new NotImplementedException();

        public Book GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public Review GetReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> StoreBookAsync(Book model)
        {
            _context.Books.Add(model);
            Task<int> task = _context.SaveChangesAsync();
            int result = await task;
            return result;
            // returns a positive value if succussful
        }
    }
}
