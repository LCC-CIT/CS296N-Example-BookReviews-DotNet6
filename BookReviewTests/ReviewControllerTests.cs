using System;
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

        [Fact]
        public void FilterByTitleTest()
        {
            // Test to see if only reviews with the selected title are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var review1 = new Review() { Book = new Book { BookTitle = "Book 1" } };
            repo.StoreReviewAsync(review1).Wait();
            repo.StoreReviewAsync(review1).Wait();
            var review2 = new Review() { Book = new Book { BookTitle = "Book 2" } };
            repo.StoreReviewAsync(review2).Wait();
            repo.StoreReviewAsync(review2).Wait();
            var review3 = new Review() { Book = new Book { BookTitle = "Book 3" } };
            repo.StoreReviewAsync(review3).Wait();
            repo.StoreReviewAsync(review3).Wait();

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
        public void FilterByReviewerTest()
        {
            // Test to see if only reviews with the selected title are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var review1 = new Review() { Reviewer = new AppUser() { Name = "Reviewer 1" }, Book = new Book() };
            repo.StoreReviewAsync(review1);
            repo.StoreReviewAsync(review1);
            var review2 = new Review() { Reviewer = new AppUser() { Name = "Reviewer 2" }, Book = new Book() };
            repo.StoreReviewAsync(review2);
            repo.StoreReviewAsync(review2);
            var review3 = new Review() { Reviewer = new AppUser() { Name = "Reviewer 3" }, Book = new Book() };
            repo.StoreReviewAsync(review3);
            repo.StoreReviewAsync(review3);

            var controller = new ReviewController(repo, null);  // I don't need a UserManager

            // Act
            var filteredReviewsView = controller.Index(review2.Reviewer.Name, null, null).Result as ViewResult;
            List<Review> filteredReviews = filteredReviewsView.Model as List<Review>;

            // Assert
            Assert.Equal(2, filteredReviews.Count);
            Assert.Equal(filteredReviews[0].Reviewer.Name, review2.Reviewer.Name);
            Assert.Equal(filteredReviews[1].Reviewer.Name, review2.Reviewer.Name);
        }

        [Fact]
        public void FilterByDateTest()
        {
            // Test to see if only reviews with the selected title are returned 

            // Arrange
            // Done in the constructor

            // We don't need need to add all the properties to the models since we aren't testing that.
            var review1 = new Review() { Book = new Book(), ReviewDate = DateTime.Parse("01/01/2020") };
            repo.StoreReviewAsync(review1);
            repo.StoreReviewAsync(review1);
            var review2 = new Review() { Book = new Book(), ReviewDate = DateTime.Parse("06/15/2021") };
            repo.StoreReviewAsync(review2);
            repo.StoreReviewAsync(review2);
            var review3 = new Review() { Book = new Book(), ReviewDate = DateTime.Parse("12/31/2022") };
            repo.StoreReviewAsync(review3);
            repo.StoreReviewAsync(review3);

            var controller = new ReviewController(repo, null);  // I don't need a UserManager

            // Act
            var filteredReviewsView = controller.Index(null, null, review2.ReviewDate.ToShortDateString()).Result as ViewResult;
            List<Review> filteredReviews = filteredReviewsView.Model as List<Review>;

            // Assert
            Assert.Equal(2, filteredReviews.Count);
            Assert.Equal(filteredReviews[0].ReviewDate, review2.ReviewDate);
            Assert.Equal(filteredReviews[1].ReviewDate, review2.ReviewDate);
        }

    }
}
