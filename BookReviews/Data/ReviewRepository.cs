using BookReviews.Models;
using Microsoft.EntityFrameworkCore;

namespace BookReviews.Data
{
    public class ReviewRepository : IReviewRepository
    {
        private ApplicationDbContext context;

        public ReviewRepository(ApplicationDbContext appDbContext)
        {
            context = appDbContext;
        }

        public IQueryable<Book> Books
        {
            get
            {
                // Get all the Review objects in the Reviews DbSet
                // and include the Reivewer object in each Review.
                return context.Books.Include(b => b.Authors)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.Reviewer);
                // TODO: Include Comments                    
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
            context.Books.Add(model);
            Task<int> task = context.SaveChangesAsync();
            int result = await task;
            return result;
            // returns a positive value if succussful
        }
    }
}
