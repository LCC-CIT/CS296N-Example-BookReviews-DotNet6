using BookReviews.Data;
using BookReviews.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookReviewTests
{
    public class FakeReviewRepository : IReviewRepository
    {
        private List<Review> reviews = new List<Review>();

        IQueryable<Review> IReviewRepository.Reviews => throw new System.NotImplementedException();

        public Review GetReviewById(int id)
        {
            Review review = reviews.Find(r => r.ReviewId == id);

            return review;
        }

        public int StoreReview(Review model)
        {
            int status = 0;
            if (model != null && model.Book != null)
            {
                model.ReviewId = reviews.Count + 1;
                reviews.Add(model);
                status = 1;    
            }
            return status;
        }

    }
}
