using BookReviews.Controllers;
using BookReviews.Data;
using BookReviews.Models;
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
            var result = controller.Review(new Review {Book = new Book()});

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
            var result = controller.Review(new Review());

            // assert
            // This result is returned if the review was NOT stored successfully
            Assert.True(result.GetType() == typeof(ViewResult));
        }

    }
}
