using System;
using BookReviews;
using BookReviews.Data;
using BookReviews.Models;
using TestSupport.EfHelpers;

namespace BookReviewTests;

internal class SetupTestRepo
{
    // Create a DbContext for the SQLite in-memory database
    public static ApplicationDbContext CreateContext()
    {
        var options = SqliteInMemory.CreateOptions<ApplicationDbContext>();
        return new ApplicationDbContext(options);
    }

    public static BookRepository CreateRepo(ApplicationDbContext context)
    {
        // Create a BookRepository instance using a SQLite in-memory db
        context.Database.EnsureCreated();
        return new BookRepository(context);
    }

    public static Book CreateBook()
    {
        // Create an Author, Book and Review objects and initialize them
        var author = new Author
        {
            Name = "J.R.R. Tolkien",
            Birthdate = DateTime.Parse("01/03/1892")
        };

        var book = new Book
        {
            BookTitle = "The Hobbit",
            Publisher = "Houghton Mifflin Books",
            Isbn = 395071224, // ISBN-10
            PubDate = DateTime.Parse("01/01/1966")
        };

        var review = new Review
        {
            Reviewer = new AppUser(),
            ReviewText = "This is not a real review.",
            ReviewDate = DateTime.Parse("01/01/2000")
        };

        var comment = new Comment
        {
            CommentText = "This is the first test comment",
            Commenter = new AppUser()
        };

        review.Comments.Add(comment);
        book.Authors.Add(author);
        book.Reviews.Add(review);

        return book;
    }


}