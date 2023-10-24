using System.Collections.Generic;
using System.Linq;
using BookReviews;
using BookReviews.Controllers;
using BookReviews.Data;
using BookReviews.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BookReviewTests;

public class ReviewControllerTests
{
    private readonly ApplicationDbContext context;
    private readonly ReviewController controller;
    private readonly BookRepository repo;

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
        var book1 = new Book { BookTitle = "Book 1" };
        var review1 = new Review
        {
            ReviewText = "Test text 1 for book 1",
            Reviewer = new AppUser()
        };
        book1.Reviews.Add(review1);
        context.Add(book1);
        var book2 = new Book { BookTitle = "Book 2" };
        var review2 = new Review
        {
            ReviewText = "Test text 2 for book 2",
            Reviewer = new AppUser()
        };
        book2.Reviews.Add(review2);
        var review3 = new Review
        {
            ReviewText = "Test text 3 for book 2",
            Reviewer = new AppUser()
        };
        book2.Reviews.Add(review3);
        context.Add(book2);
        var book3 = new Book { BookTitle = "Book 3" };
        var review4 = new Review
        {
            ReviewText = "Test text 4 for book 2",
            Reviewer = new AppUser()
        };
        book3.Reviews.Add(review4);
        context.Add(book3);
        context.SaveChanges();

        var controller = new ReviewController(repo, null); // I don't need a UserManager

        // Act
        var filteredBooksView = controller.Index(null, book2.BookTitle, null).Result as ViewResult;
        var filteredBooks = filteredBooksView.Model as List<Book>;

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
    
    /* Can't test this without a UserManager
    // Test adding a comment to a review using the Comment action method
    [Fact]
    void CommentTest()
    {
        // Arrange
        var book = SetupTestRepo.CreateBook();
        context.Add(book);
        context.SaveChanges();
        const string COMMENT_TEXT = "This should be the second test comment";
        CommentVM vm = new()
        {
            BookId = book.BookId, ReviewId = book.Reviews.First().ReviewId,
            CommentText = COMMENT_TEXT
        };
        
        // Act
        var result = controller.Comment(vm).Result;

        // Assert comment was added to the review in the db
        var review = context.Reviews.First();
        // Assert the review has two comments
        Assert.Equal(2, review.Comments.Count);
        // Assert the second comment is the one we added
        Assert.Equal(COMMENT_TEXT, review.Comments.Last().CommentText);
        // Assert the result is a redirect to the Index action
        Assert.True(result.GetType() == typeof(RedirectToActionResult));
        // Assert that the result contains the correct bookId
        Assert.Equal(book.BookId, ((RedirectToActionResult)result).RouteValues["bookId"]);
        //  Assert that the result contains the correct reviewId
        Assert.Equal(book.Reviews.First().ReviewId, ((RedirectToActionResult)result).RouteValues["reviewId"]);
    }
    */
}