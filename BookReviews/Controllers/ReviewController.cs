using BookReviews.Data;
using BookReviews.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReviews.Controllers
{
    public class ReviewController : Controller
    {
        IReviewRepository repo;
        UserManager<AppUser> userManager;
        public ReviewController(IReviewRepository r, UserManager<AppUser> userMngr)
        {
            repo = r;
            userManager = userMngr;
        }

        public async Task<IActionResult> Index(String reviewerName, String bookTitle, String reviewDate)
        {
            List<Review> reviews = new();
            /*
            // filter by reviewer name
            if (reviewerName != null)
            {
                reviews = await ReviewerQuery(reviewerName).ToListAsync<Review>();
            }
            // filter by book title
            else if (bookTitle != null)
            {
                reviews = await TitleQuery(bookTitle).ToListAsync<Review>();
            }
            // filter by review date
            else if (reviewDate != null)
            {
                reviews = await DateQuery(reviewDate).ToListAsync<Review>();
            }
            // All query parameters are null
            else
            */
            {
                // Get all the books, then get all the reviews and put them in a list
                // TODO: Get a queryable of the books instead of a list
                var books = await repo.Books.ToListAsync<Book>();
                foreach (Book book in books)
                {
                    foreach (Review review in book.Reviews)
                    {
                        reviews.Add(review);
                    }
                }
            }

            return View(reviews);
        }

        [Authorize]
        public IActionResult Review()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Review(Review model)
        {
            /*
            // TODO: Refactor to use Book model as root
            // Get the AppUser object for the currently logged in user
            // For unit testing, UserManager will be null so accomodate that
            if (userManager != null)
            {
                model.Reviewer = await userManager.GetUserAsync(User);
            }
            if (await repo.StoreBookAsync(model) > 0)
            {
                return RedirectToAction("Index", new { reviewId = model.ReviewId });
            }
            else
            {
                return View();  // TODO: Send an error message to the view
            }
            */
            return View();
        }
    }
}
