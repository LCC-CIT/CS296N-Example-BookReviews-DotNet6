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
        IReviewRepository _repo;
        UserManager<AppUser> _userManager;
        public ReviewController(IReviewRepository r, UserManager<AppUser> userMngr)
        {
            _repo = r;
            _userManager = userMngr;
        }

        public async Task<IActionResult> Index(String reviewerName, String bookTitle, String reviewDate)
        {
            var bookQueryable = _repo.Books;
            List<Book> books;

            // filter by book title
            if (bookTitle != null)
            {
              books = await bookQueryable.Where(b => b.BookTitle == bookTitle).ToListAsync<Book>();
            }
            else
            {
                books = await bookQueryable.ToListAsync<Book>();
            }

            return View(books);
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
