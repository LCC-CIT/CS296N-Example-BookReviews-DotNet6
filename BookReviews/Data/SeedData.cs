using BookReviews.Models;
using Microsoft.AspNetCore.Identity;

namespace BookReviews.Data
{
    public class SeedData
    {
     public static void Seed(ApplicationDbContext context, IServiceProvider provider)
        {
            if (!context.Reviews.Any())  // this is to prevent duplicate data from being added
            {
                var userManager = provider.GetRequiredService<UserManager<AppUser>>();

                Book book = new Book { AuthorName = "Samuel Shallabarger",
                    BookTitle = "Prince of Foxes"
                };
                context.Books.Add(book);
                context.SaveChanges();

                AppUser emmaWatson = userManager.FindByNameAsync("EmmaWatson").Result;
                Review review = new Review
                {
                    Book = book,
                    ReviewText = "Great book, a must read!",
                    Reviewer = emmaWatson,
                    ReviewDate = DateTime.Parse("11/1/2020")
                };
                context.Reviews.Add(review);  // queues up the review to be added to the DB

                AppUser danielRadcliffe = userManager.FindByNameAsync("DanielRadcliffe").Result;
                review = new Review
                {
                    Book = book,
                    ReviewText = "I love the clever, witty dialog",
                    Reviewer = danielRadcliffe,
                    ReviewDate = DateTime.Parse("11/15/2020")
                };
                context.Reviews.Add(review);

                context.SaveChanges();

                AppUser brianBird = userManager.FindByNameAsync("BrianBird").Result;
                review = new Review
                {
                    Book = new Book
                    {
                        BookTitle = "Virgil Wander",
                        AuthorName = "Lief Enger"
                    },
                    ReviewText = "Wonderful book, written by a distant cousin of mine.",
                    Reviewer = brianBird,
                    ReviewDate = DateTime.Parse("11/30/2020")
                };
                context.Reviews.Add(review);

                review = new Review
                {
                    Book = new Book
                    {
                        BookTitle = "Ivanho",
                        AuthorName = "Sir Walter Scott"
                    },
                    ReviewText = "It was a little hard going at first, but then I loved it!",
                    Reviewer =brianBird,
                    ReviewDate = DateTime.Parse("11/1/2020")
                };
                context.Reviews.Add(review);

                context.SaveChanges(); // stores all the reviews in the DB
            
            }
        }
    }
}
