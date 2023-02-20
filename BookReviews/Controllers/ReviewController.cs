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
            List<Review> reviews;

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
            {
                reviews = await repo.Reviews.ToListAsync<Review>();
            }

            return View(reviews);
        }

        public IQueryable<Review> TitleQuery(string bookTitle)
        {
            return repo.Reviews
                .Where(r => r.Book.BookTitle == bookTitle)
                .Select(r => r);
        }

        public IQueryable<Review> ReviewerQuery(string reviewerName)
        {
            return repo.Reviews
                .Where(r => r.Reviewer.Name == reviewerName)
                .Select(r => r);
        }

        public IQueryable<Review> DateQuery(string reviewDate)
        {
            return repo.Reviews
                   .Where(r => r.ReviewDate.Date == DateTime.Parse(reviewDate).Date)
                   .Select(r => r);
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
            // Get the AppUser object for the currently logged in user
            // For unit testing, UserManager will be null so accomodate that
            if (userManager != null)
            {
                model.Reviewer = await userManager.GetUserAsync(User);
            }
            if (await repo.StoreReviewAsync(model) > 0)
            {
                return RedirectToAction("Index", new { reviewId = model.ReviewId });
            }
            else
            {
                return View();  // TODO: Send an error message to the view
            }

        }
    }
}
