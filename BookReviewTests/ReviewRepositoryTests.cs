using BookReviews;
using BookReviews.Data;
using BookReviews.Models;
using System.Linq;
using Xunit;

namespace BookReviewTests
{
    public class ReviewRepositoryTests
    {
        private ApplicationDbContext context;
        private ReviewRepository repo;
        private Book book;

        public ReviewRepositoryTests()
        {
            context = SetupTestRepo.CreateContext();
            repo = SetupTestRepo.CreateRepo(context);
            book = SetupTestRepo.CreateBook();
        }

        // Tests

        [Fact]
        // Verify that a book object can be stored in the database
        public void StoreBookTest()
        {
            // Arrange: done in constructor
            //Act
            int result = repo.StoreBookAsync(book).Result;
            // Assert
            Assert.True(result > 0);
            Assert.Equal(1, context.Books.Count());
        }

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
            Assert.NotNull(bookFromRepo.Reviews.First().Comments.First().Commentor);
        }
    }
}
