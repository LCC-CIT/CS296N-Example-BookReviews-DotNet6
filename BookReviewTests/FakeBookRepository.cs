using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookReviews.Data;
using BookReviews.Models;

namespace BookReviewTests;

public class FakeBookRepository : IBookRepository
{
    private readonly List<Book> books = new();


    public IQueryable<Book> Books => throw new NotImplementedException();

    public IQueryable<Review> Reviews => throw new NotImplementedException();

    public Book GetBookById(int id)
    {
        throw new NotImplementedException();
    }

    public Review GetReviewById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Book> GetBookByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddOrUpdateBookAsync(Book model)
    {
        var status = 0;
        if (model != null)
        {
            model.BookId = books.Count + 1;
            books.Add(model);
            status = 1;
        }

        return status;
    }
}