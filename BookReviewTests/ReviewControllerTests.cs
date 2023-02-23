using System.Collections.Generic;
using System.Linq;
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
        IReviewRepository repo = new FakeReviewRepository();
        ReviewController controller;

        public ReviewControllerTests()
        {
            controller = new ReviewController(repo, null); // not passsing in UserManager
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

        /*
        [Fact]
        public void FilterByTitleTest()
        {
            // Test to see if only reviews with the selected title are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var review1 = new Review() { Book = new Book { BookTitle = "Book 1" } };
            repo.StoreBookAsync(review1).Wait();
            repo.StoreBookAsync(review1).Wait();
            var review2 = new Review() { Book = new Book { BookTitle = "Book 2" } };
            repo.StoreBookAsync(review2).Wait();
            repo.StoreBookAsync(review2).Wait();
            var review3 = new Review() { Book = new Book { BookTitle = "Book 3" } };
            repo.StoreBookAsync(review3).Wait();
            repo.StoreBookAsync(review3).Wait();

            var controller = new ReviewController(repo, null);  // I don't need a UserManager

            // Act
            var filteredReviewsView = controller.Index(null, review2.Book.BookTitle, null).Result as ViewResult;
            List<Review> filteredReviews = filteredReviewsView.Model as List<Review>;

            // Assert
            Assert.Equal(2, filteredReviews.Count);
            Assert.Equal(filteredReviews[0].Book.BookTitle, review2.Book.BookTitle);
            Assert.Equal(filteredReviews[1].Book.BookTitle, review2.Book.BookTitle);
        }

        
        [Fact]
        public void FilterByTitleTest()
        {
            // I'm just testing the query, not the controller method, because the query does all the work.
            // Test to see if only reviews with the selected title are returned 

            // Arrange
            var reviews = new List<Review>();
            // We don't need need to add all the properties to the models since we aren't testing that.
            var review1 = new Review() { Book = new Book { BookTitle = "Book 1" } };
            reviews.Add(review1);
            reviews.Add(review1);
            var review2 = new Review() { Book = new Book { BookTitle = "Book 2" } };
            reviews.Add(review2);
            reviews.Add(review2);
            var review3 = new Review() { Book = new Book { BookTitle = "Book 3" } };
            reviews.Add(review3);
            reviews.Add(review3);

            var controller = new ReviewController(repo, null);  // I don't need a UserManager

            // Act
            var filteredReviews = controller.TitleQuery(review2.Book.BookTitle).ToList<Review>();

            // Assert
            Assert.Equal(2, filteredReviews.Count);
            Assert.Equal(filteredReviews[0].Book.BookTitle, review2.Book.BookTitle);
            Assert.Equal(filteredReviews[1].Book.BookTitle, review2.Book.BookTitle);
        }

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
            var review3 = new Review() { Reviewer = new AppUser() { Name = "Reviewer 3" }, Book = new Book() };
            repo.StoreBookAsync(review3);
            repo.StoreBookAsync(review3);

            var controller = new ReviewController(repo, null);  // I don't need a UserManager

            // Act
            var filteredReviews = controller.ReviewerQuery(review2.Reviewer.Name).ToList<Review>();

            // Assert
            Assert.Equal(2, filteredReviews.Count);
            Assert.Equal(filteredReviews[0].Reviewer.Name, review2.Reviewer.Name);
            Assert.Equal(filteredReviews[1].Reviewer.Name, review2.Reviewer.Name);
        }
        */

        // Note: The Index method is not being tested because it doesn't do much
        // processing; it just calls another method on the repository.
       
    }
}
