using BookReviews.Data;
using BookReviews.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReviewTests
{
    public class FakeReviewRepository : IReviewRepository
    {

        private List<Book> _books = new List<Book>();


 

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
                model.BookId = _books.Count + 1;
                _books.Add(model);
                status = 1;    
            }
            return status;
        }

    }
}
