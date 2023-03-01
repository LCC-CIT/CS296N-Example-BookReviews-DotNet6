using BookReviews.Models;

namespace BookReviews.Data
{
    public interface IReviewRepository
    {
        // TODO: Remove the Review related methods
        IQueryable<Review> Reviews { get; }  // Read (or retrieve) reviews
        public Review GetReviewById(int id);
        IQueryable<Book> Books { get; }  // Read (or retrieve) books
        public Book GetBookById(int id);
        public Task<int> StoreBookAsync(Book model);
    }
}
