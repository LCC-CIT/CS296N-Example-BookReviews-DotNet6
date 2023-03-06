using BookReviews.Data;
using BookReviews.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReviews.Controllers;

public class ReviewController : Controller
{
    private readonly IBookRepository repo;
    private readonly UserManager<AppUser> userManager;

    public ReviewController(IBookRepository r, UserManager<AppUser> userMngr)
    {
        repo = r;
        userManager = userMngr;
    }

    public async Task<IActionResult> Index(string reviewerName, string bookTitle, string reviewDate)
    {
        var bookQueryable = repo.Books;
        List<Book> books;

        // filter by book title
        if (bookTitle != null)
            books = await bookQueryable
                .Where(b => b.BookTitle == bookTitle)
                .ToListAsync();
        // filter by reviewer name
        else if (reviewerName != null)
            books = await bookQueryable
                .Where(b => b.Reviews
                    .Any(r => r.Reviewer.Name == reviewerName))
                .ToListAsync();
        // Note: This query will return all books with at least one
        // review by the specified reviewer.
        // Linq operator Any returns true if condition is true for any Review
        // filter by review date
        else if (reviewDate != null)
            books = await bookQueryable
                .Where(b => b.Reviews
                    .Any(r => r.ReviewDate.Date == DateTime.Parse(reviewDate).Date))
                .ToListAsync();
        else
            books = await bookQueryable.ToListAsync();

        return View(books);
    }


    [Authorize]
    public async Task<IActionResult> Review(int id)
    {
        // Get the book that this review is for
        var book = await repo.GetBookByIdAsync(id);
        return View(book);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Review(string reviewText, int bookId)
    {
        var review = new Review { ReviewText = reviewText };
        // For unit testing, UserManager will be null so accomodate that
        if (userManager != null)
        {
            // Get the AppUser object for the currently logged in user
            review.Reviewer = await userManager.GetUserAsync(User);
        }
        // Get the book that this review is for
        var book = await repo.GetBookByIdAsync(bookId);
        // Add the review to the book
        book.Reviews.Add(review);
        // Save the book to the database
        if (await repo.AddOrUpdateBookAsync(book) > 0)
        {
            return RedirectToAction("Index", new { reviewId = review.ReviewId });
        }
        return View();  // TODO: Send an error message to the view
    }

    // Open the form for entering a comment
    [Authorize]
    public IActionResult Comment(int reviewId, int bookId)
    {
        var commentVM = new CommentVM { ReviewId = reviewId, BookId = bookId };
        return View(commentVM);
    }

    [HttpPost]
    public async Task<RedirectToActionResult> Comment(CommentVM commentVM)
    {
        // Comment is the domain model
        var comment = new Comment { CommentText = commentVM.CommentText };
        if (userManager != null)
        {
            comment.Commenter = await userManager.GetUserAsync(User);
            comment.Commenter.Name = comment.Commenter.UserName;  // TODO: Get the user's name at registration
        }
        // Retrieve the book that this comment is for
        var book = await repo.GetBookByIdAsync(commentVM.BookId);
        
        // Retrieve the review that this comment is for
        var review = book.Reviews.FirstOrDefault(r => r.ReviewId == commentVM.ReviewId);

        // Store the book, review and comment in the database
        review.Comments.Add(comment);
        await repo.AddOrUpdateBookAsync(book);

        return RedirectToAction("Index", new { bookTitle = book.BookTitle });
    }
}