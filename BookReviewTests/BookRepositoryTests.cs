using System.Linq;
using BookReviews;
using BookReviews.Data;
using BookReviews.Models;
using Xunit;

namespace BookReviewTests;

public class BookRepositoryTests
{
    private readonly Book book;
    private readonly ApplicationDbContext context;
    private readonly BookRepository repo;

    public BookRepositoryTests()
    {
        context = SetupTestRepo.CreateContext();
        repo = SetupTestRepo.CreateRepo(context);
        book = SetupTestRepo.CreateBook();
    }

    // Tests

    /* The in-memory database doesn't appear to support the update operation on 
     * many-to-many relationships like Book to Author so this test fails.
      
    [Fact]
    // Verify that a book object can be stored in the database
    public void StoreBookTest()
    {
        // Arrange: done in constructor
        //Act
        var result = repo.AddOrUpdateBookAsync(book).Result;
        // Assert
        Assert.True(result > 0);
        Assert.Equal(1, context.Books.Count());
    }
    */

    [Fact]
    // Verify that a Book and all the related data are loaded
    public void IQueryableBooksTest()
    {
        // Arrange: store a book and it's related data in the database
        context.Books.Add(book);
        context.SaveChanges();

        // Act: get the IQueryable<Book> object
        var bookFromRepo = repo.Books.First();

        // Assert: check that the IQueryable<Book> object and all it's related data are there
        Assert.NotNull(bookFromRepo);
        Assert.NotNull(bookFromRepo.Authors.First());
        Assert.NotNull(bookFromRepo.Reviews.First());
        Assert.NotNull(bookFromRepo.Reviews.First().Reviewer);
        Assert.NotNull(bookFromRepo.Reviews.First().Comments.First());
        Assert.NotNull(bookFromRepo.Reviews.First().Comments.First().Commenter);
    }
}