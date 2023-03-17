using System;
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
    private readonly ApplicationDbContext _context;
    private readonly ReviewController _controller;
    private readonly BookRepository _repo;

    // variables and constants used in tests
    Book _book1, _book2, _book3, _book4;
    Review _review1, _review2, _review3, _review4, _review5;
    AppUser _reviewer1, _reviewer2, _reviewer3;
    DateTime DATE_3_4_2014 = DateTime.Parse("3/4/2014");
    DateTime DATE_5_10_2019 = DateTime.Parse("5/10/2019");
    DateTime DATE_4_12_2023 = DateTime.Parse("4/12/2023");

    public ReviewControllerTests()
    {
        _context = SetupTestRepo.CreateContext();
        _repo = SetupTestRepo.CreateRepo(_context);
        _controller = new ReviewController(_repo, null); // we don't need the UserManager
    }

    
    [Fact]
    public void Review_PostTest_Success()
    {
        // arrange
        var book = SetupTestRepo.CreateBook();
        _context.Add(book);
        _context.SaveChanges();

        // act
        var result = _controller.Review("This is a test review", 1).Result;

        // assert
        // This result is returned if the review was stored successfully
        Assert.True(result.GetType() == typeof(RedirectToActionResult));
    }

    [Fact]
    public void Review_PostTest_Failure()
    {
        // arrange
        var book = SetupTestRepo.CreateBook();
        _context.Add(book);
        _context.SaveChanges();

        // act
        // The review will NOT be stored successfully without a valid BookId
        var result = _controller.Review("This is a test review", 0).Result;

        // assert
        // This result is returned if the review was NOT stored successfully
        Assert.True(result.GetType() == typeof(ViewResult));
    }


    [Fact]
    // Put a book into the db and verify that it is returned by the Index method with no filtering
    public void IndexTest()
    {
        // arrange
        var book = SetupTestRepo.CreateBook();
        _context.Add(book);
        _context.SaveChanges();

        // act
        var viewResult = _controller.Index(null, null, null).Result as ViewResult;

        // assert
        var books = viewResult.Model as List<Book>;
        Assert.Single(books);
    }

    [Fact]
    public void FilterByTitleTest()
    {
        createBooksReviewsAndReviewers();
        var controller = new ReviewController(_repo, null); // I don't need a UserManager

        // Act
        var filteredBooksView = controller.Index(null, _book2.BookTitle, null).Result as ViewResult;
        var filteredBooks = filteredBooksView.Model as List<Book>;

        // Assert
        // Just the second book, with two reviews should have been returned
        Assert.Single(filteredBooks);
        Assert.Equal(filteredBooks.First().BookTitle, _book2.BookTitle);
        Assert.Equal(filteredBooks.First().Reviews[0], _book2.Reviews[0]);
        Assert.Equal(filteredBooks.First().Reviews[1], _book2.Reviews[1]);
    }
    
    [Fact]
    public void FilterByReviewerTest()
    {
        // Test to see if only books reviewed by the selected reviewer are returned 

        // Arrange
        createBooksReviewsAndReviewers();


        var controller = new ReviewController(_repo, null);  // I don't need a UserManager

        // Act
        var filteredBooksView = controller.Index(_review2.Reviewer.Name, null, null).Result as ViewResult;
        var filteredBooks = filteredBooksView.Model as List<Book>;
        // Assert
        Assert.Equal(2, filteredBooks.Count);
        Assert.NotNull(filteredBooks[0].Reviews
            .Where(r => r.Reviewer.Name == _review2.Reviewer.Name)
            .FirstOrDefault());
        Assert.NotNull(filteredBooks[1].Reviews
            .Where(r => r.Reviewer.Name == _review2.Reviewer.Name)
            .FirstOrDefault());
    }

    [Fact]
    public void FilterByDateTest()
    {
        // Test to see if only books reviewed on the selected date are returned 

        // Arrange
        createBooksReviewsAndReviewers();

        var controller = new ReviewController(_repo, null);  // I don't need a UserManager

        // Act
        var filteredBooksView = controller.Index(null, null, DATE_5_10_2019.ToShortDateString()).Result as ViewResult;
        var filteredBooks = filteredBooksView.Model as List<Book>;
        // Assert
        Assert.Equal(2, filteredBooks.Count);
        Assert.NotNull(filteredBooks[0].Reviews
            .Where(r => r.ReviewDate == DATE_5_10_2019)
            .FirstOrDefault());
        Assert.NotNull(filteredBooks[1].Reviews
            .Where(r => r.ReviewDate == DATE_5_10_2019)
            .FirstOrDefault());
    }

    private void createBooksReviewsAndReviewers()
    {
        // We don't need need to add all the properties to the models
        _book1 = new Book { BookTitle = "Book 1" };
        _reviewer1 = new AppUser { Name = "Reviewer One" };
        _review1 = new Review
        {
            ReviewText = "Test text 1 for book 1",
            Reviewer = _reviewer1,
            ReviewDate = DATE_3_4_2014
        };
        _book1.Reviews.Add(_review1);
        _context.Add(_book1);

        _book2 = new Book { BookTitle = "Book 2" };
        _review2 = new Review
        {
            ReviewText = "Test text 2 for book 2",
            Reviewer = _reviewer1,
            ReviewDate = DATE_5_10_2019
        };
        _book2.Reviews.Add(_review2);
        _reviewer2 = new AppUser { Name = "Reviewer Two" };
        _review3 = new Review
        {
            ReviewText = "Test text 3 for book 2",
            Reviewer = _reviewer2,
             ReviewDate = DATE_3_4_2014
        };
        _book2.Reviews.Add(_review3);
        _context.Add(_book2);

        _reviewer3 = new AppUser { Name = "Reviewer Three" };
        _book3 = new Book { BookTitle = "Book 3" };
        _review4 = new Review
        {
            ReviewText = "Test text 4 for book 3",
            Reviewer = _reviewer3,
            ReviewDate = DATE_5_10_2019
        };
        _book3.Reviews.Add(_review4);
        _context.Add(_book3);

        _book4 = new Book { BookTitle = "Book 4" };
        _review5 = new Review
        {
            ReviewText = "Test text 5 for book 4",
            Reviewer = _reviewer3,
            ReviewDate = DATE_4_12_2023
        };
        _book4.Reviews.Add(_review5);
        _context.Add(_book4);
        _context.SaveChanges();
    }
}