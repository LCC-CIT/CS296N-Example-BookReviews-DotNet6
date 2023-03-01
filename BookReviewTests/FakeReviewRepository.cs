using BookReviews.Data;
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

        private List<Book> books = new List<Book>();


 

        public IQueryable<Book> Books => throw new System.NotImplementedException();

        public IQueryable<Review> Reviews => throw new System.NotImplementedException();

        public Book GetBookById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Review GetReviewById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> StoreBookAsync(Book model)
        {
            int status = 0;
            if (model != null)
            {
                model.BookId = books.Count + 1;
                books.Add(model);
                status = 1;    
            }
            return status;
        }

    }
}
