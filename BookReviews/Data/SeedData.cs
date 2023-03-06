using BookReviews.Models;
using Microsoft.AspNetCore.Identity;

namespace BookReviews.Data;

public class SeedData
{
    public static void Seed(ApplicationDbContext context, IServiceProvider provider)
    {
        if (!context.Books.Any()) // this is to prevent duplicate data from being added
        {
            var userManager = provider.GetRequiredService<UserManager<AppUser>>();

            // There will be two reviews of this book in the seed data
            var book = new Book { BookTitle = "Prince of Foxes" };
            book.Authors.Add(new Author { Name = "Samuel Shallabarger" });

            // First review
            var emmaWatson = userManager.FindByNameAsync("EmmaWatson").Result;
            var review = new Review
            {
                ReviewText = "Great book, a must read!",
                Reviewer = emmaWatson,
                ReviewDate = DateTime.Parse("11/1/2020")
            };
            book.Reviews.Add(review);

            // Second review of Prince of Foxes
            var danielRadcliffe = userManager.FindByNameAsync("DanielRadcliffe").Result;
            review = new Review
            {
                ReviewText = "I love the clever, witty dialog",
                Reviewer = danielRadcliffe,
                ReviewDate = DateTime.Parse("11/15/2020")
            };
            book.Reviews.Add(review);
            context.Books.Add(book);

            // Third review, another book
            var brianBird = userManager.FindByNameAsync("BrianBird").Result;
            book = new Book { BookTitle = "Virgil Wander" };
            book.Authors.Add(new Author { Name = "Lief Enger" });
            review = new Review
            {
                ReviewText = "Wonderful book, written by a distant cousin of mine.",
                Reviewer = brianBird,
                ReviewDate = DateTime.Parse("11/30/2020")
            };
            book.Reviews.Add(review);
            context.Books.Add(book);

            // fourth review, another book
            book = new Book { BookTitle = "Ivanho" };
            book.Authors.Add(new Author { Name = "Sir Walter Scott" });
            review = new Review
            {
                ReviewText = "It was a little hard going at first, but then I loved it!",
                Reviewer = brianBird,
                ReviewDate = DateTime.Parse("11/1/2020")
            };
            book.Reviews.Add(review);
            context.Books.Add(book);

            // Another book and the fifth review
            book = new Book { BookTitle = "The Hobbit" };
            book.Authors.Add(new Author { Name = "J.R.R. Tolkien" });
            review = new Review
            {
                ReviewText = "This is a classic that lives up to its reputation!",
                Reviewer = brianBird,
                ReviewDate = DateTime.Parse("09/22/2020")
            };
            book.Reviews.Add(review);
            context.Books.Add(book);

            // Another book and the sixth review
            book = new Book { BookTitle = "Murach's ASP.NET Core MVC" };
            book.Authors.Add(new Author { Name = "Joel Murach" });
            book.Authors.Add(new Author { Name = "Mary Delameter" });
            review = new Review
            {
                ReviewText = "This is a great book for learning MVC!",
                Reviewer = brianBird,
                ReviewDate = DateTime.Parse("11/1/2020")
            };
            book.Reviews.Add(review);
            context.Books.Add(book);

            context.SaveChanges(); // stores all the books with thier reviews in the DB
        }
    }
}