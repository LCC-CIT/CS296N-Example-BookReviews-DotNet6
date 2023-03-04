using System.Collections.Generic;
using System.Linq;
using BookReviews;
using BookReviews.Controllers;
using BookReviews.Data;
using BookReviews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BookReviewTests
{
    public class ReviewControllerTests
    {
        private ApplicationDbContext context;
        private ReviewRepository repo;
        private ReviewController controller;

        public ReviewControllerTests()
        {
            context = SetupTestRepo.CreateContext();
            repo = SetupTestRepo.CreateRepo(context);
            controller = new ReviewController(repo, null); // we don't need the UserManager
        }
        /*
        [Fact]
        public void Review_PostTest_Success()
        {
            // arrange
            // Done in the constructor

            // act
            var result = controller.Review(new Review {Book = new Book()}).Result;

            // assert
            // This result is returned if the review was stored successfully
            Assert.True(result.GetType() == typeof(RedirectToActionResult));
        }

        [Fact]
        public void Review_PostTest_Failure()
        {
            // arrange
            // Done in the constructor

            // act
            // The review will NOT be stored successfully without a Book object
            var result = controller.Review(new Review()).Result;

            // assert
            // This result is returned if the review was NOT stored successfully
            Assert.True(result.GetType() == typeof(ViewResult));
        }
        */
        [Fact]
        // Put a book into the db and verify that it is returned by the Index method with no filtering
        public void IndexTest()
        {
            // arrange
            var book = SetupTestRepo.CreateBook();
            context.Add(book);
            context.SaveChanges();

            // act
            var viewResult = controller.Index(null, null, null).Result as ViewResult;

            // assert
            var books = viewResult.Model as List<Book>;
            Assert.Single(books);

        }   

        [Fact]
        public void FilterByTitleTest()
        {
            // Test to see if only the book and reviews with the selected title are returned 

            // Arrange
            // We don't need need to add all the properties to the models
            var book1 = new Book() { BookTitle = "Book 1" };
            var review1 = new Review() {ReviewText = "Test text 1 for book 1", 
                Reviewer = new AppUser()};
            book1.Reviews.Add(review1);
            context.Add(book1);
            var book2 = new Book() { BookTitle = "Book 2" };
            var review2 = new Review() {ReviewText = "Test text 2 for book 2", 
                Reviewer = new AppUser()};
            book2.Reviews.Add(review2);
            var review3 = new Review() {ReviewText = "Test text 3 for book 2", 
                Reviewer = new AppUser()};
            book2.Reviews.Add(review3);
            context.Add(book2);
            var book3 = new Book() { BookTitle = "Book 3" };
            var review4 = new Review() {ReviewText = "Test text 4 for book 2", 
                Reviewer = new AppUser()};
            book3.Reviews.Add(review4);
            context.Add(book3);
            context.SaveChanges();

            var controller = new ReviewController(repo, null);  // I don't need a UserManager

            // Act
            var filteredBooksView = controller.Index(null, book2.BookTitle, null).Result as ViewResult;
            List<Book> filteredBooks = filteredBooksView.Model as List<Book>;

            // Assert
            // Just the second book, with two reviews should have been returned
            Assert.Single(filteredBooks);
            Assert.Equal(filteredBooks.First().BookTitle, book2.BookTitle);
            Assert.Equal(filteredBooks.First().Reviews[0], book2.Reviews[0]);
            Assert.Equal(filteredBooks.First().Reviews[1], book2.Reviews[1]);
        }

        /*
        [Fact]
        public void FilterByReviewerTest()
        {
            // Test to see if only reviews with the selected title are returned 

            // Arrange
            var reviews = new List<Review>();
            // We don't need need to add all the properties to the models since we aren't testing that.
            var review1 = new Review() { Reviewer = new AppUser() { Name = "Reviewer 1" }, Book = new Book() };
            repo.StoreBookAsync(review1);
            repo.StoreBookAsync(review1);
            var review2 = new Review() { Reviewer = new AppUser() { Name = "Reviewer 2" }, Book = new Book() };
            repo.StoreBookAsync(review2);
            repo.StoreBookAsync(review2);
            var review4 = new Review() { Reviewer = new AppUser() { Name = "Reviewer 3" }, Book = new Book() };
            repo.StoreBookAsync(review4);
            repo.StoreBookAsync(review4);

            var controller = new ReviewController(repo, null);  // I don't need a UserManager

            // Act
            var filteredBooks = controller.ReviewerQuery(review2.Reviewer.Name).ToList<Review>();

            // Assert
            Assert.Equal(2, filteredBooks.Count);
            Assert.Equal(filteredBooks[0].Reviewer.Name, review2.Reviewer.Name);
            Assert.Equal(filteredBooks[1].Reviewer.Name, review2.Reviewer.Name);
        }
        */

        // Note: The Index method is not being tested because it doesn't do much
        // processing; it just calls another method on the repository.
       
    }
}
