using BookReviews.Models;

namespace BookReviews.Data;

public interface IBookRepository
{
    // TODO: Remove the Review related methods
    IQueryable<Review> Reviews { get; } // Read (or retrieve) reviews
    IQueryable<Book> Books { get; } // Read (or retrieve) books
    public Task<Review?> GetReviewByIdAsync(int id);
    public Task<Book?> GetBookByIdAsync(int id);
    public Task<int> AddOrUpdateBookAsync(Book model);
}