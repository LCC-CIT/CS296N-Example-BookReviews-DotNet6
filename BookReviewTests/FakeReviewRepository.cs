﻿using BookReviews.Data;
using BookReviews.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDoubles;

namespace BookReviewTests
{
    public class FakeReviewRepository : IReviewRepository
    {

        private List<Review> reviews = new List<Review>();


        public IQueryable<Review> Reviews
        {
            get
            {
                return new InMemoryAsyncQueryable<Review>(reviews);
            }
        }

        public Review GetReviewById(int id)
        {
            Review review = reviews.Find(r => r.ReviewId == id);

            return review;
        }

        public async Task<int> StoreReviewAsync(Review model)
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
