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

            // reviewerName is not null
            if (reviewerName != null)
            {
                /* reviews = await ((
                from r in repo.Reviews
                where r.Reviewer.UserName == reviewerName
                select r).ToListAsync<Review>();
                */

                Task<List<Review>> reviewsTask = (
                     from r in repo.Reviews
                     where r.Reviewer.UserName == reviewerName
                     select r
                     ).ToListAsync<Review>();
                 reviews = await reviewsTask;

            }
            // bookTitle is not null
            else if (bookTitle != null)
            {
                reviews = (
                    from r in repo.Reviews
                    where r.Book.BookTitle == bookTitle
                    select r
                    ).ToList<Review>();
            }
            // reviewDate is not null
            else if (reviewDate != null)
            {
                reviews = (
                    from r in repo.Reviews
                    where r.ReviewDate.Date == DateTime.Parse(reviewDate).Date
                    select r
                    ).ToList<Review>();
            }
            // Both query parameters are null
            else
            {
                reviews = repo.Reviews.ToList<Review>();
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
            // Get the AppUser object for the current user
            // For unit testing, there won't be a UserManager, so Reviewer will be null
            model.Reviewer = await userManager?.GetUserAsync(User);
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
