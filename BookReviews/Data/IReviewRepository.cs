using BookReviews.Models;

namespace BookReviews.Data
{
    public interface IReviewRepository
    {
        IQueryable<Review> Reviews { get; }  // Read (or retrieve) reviews
        public Review GetReviewById(int id);
        public Task<int> StoreReviewAsync(Review model);
    }
}
