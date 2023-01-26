using BookReviews.Data;
using BookReviews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index(String reviewerName, String bookTitle, String reviewDate)
        {
            List<Review> reviews;

            // reviewerName is not null
            if (reviewerName != null)
            {
                reviews = (
                    from r in repo.Reviews
                    where r.Reviewer.UserName == reviewerName
                    select r
                    ).ToList<Review>();
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



        public IActionResult Review()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Review(Review model)
        {
            // Get the AppUser object for the current user
            model.Reviewer = userManager.GetUserAsync(User).Result;
            if (repo.StoreReview(model) > 0)
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
