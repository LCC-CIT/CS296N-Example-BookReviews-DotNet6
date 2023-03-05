using BookReviews;
using BookReviews.Data;
using BookReviews.Models;
using System.Linq;
using Xunit;

namespace BookReviewTests
{
    public class ReviewRepositoryTests
    {
        private ApplicationDbContext _context;
        private ReviewRepository _repo;
        private Book _book;

        public ReviewRepositoryTests()
        {
            _context = SetupTestRepo.CreateContext();
            _repo = SetupTestRepo.CreateRepo(_context);
            _book = SetupTestRepo.CreateBook();
        }

        // Tests

        [Fact]
        // Verify that a book object can be stored in the database
        public void StoreBookTest()
        {
            // Arrange: done in constructor
            //Act
            int result = _repo.StoreBookAsync(_book).Result;
            // Assert
            Assert.True(result > 0);
            Assert.Equal(1, _context.Books.Count());
        }

        [Fact]
        // Verify that a Book and all the related data are loaded
        public void QueryableBooksTest()
        {
            // Arrange: store a book and it's related data in the database
            _context.Books.Add(_book);
            _context.SaveChanges();

            // Act: get the IQueryable<Book> object
            var bookFromRepo = _repo.Books.First();

            // Assert: check that the IQueryable<Book> object and all it's related data are there
            Assert.NotNull(bookFromRepo);
            Assert.NotNull(bookFromRepo.Authors.First());
            Assert.NotNull(bookFromRepo.Reviews.First());
            Assert.NotNull(bookFromRepo.Reviews.First().Reviewer);
            Assert.NotNull(bookFromRepo.Reviews.First().Comments.First());
            Assert.NotNull(bookFromRepo.Reviews.First().Comments.First().UserName);
        }
    }
}
