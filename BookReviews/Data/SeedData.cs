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

            // There will be two books by this author
            var samuelShallabarger = new Author
            {
                Name = "Samuel Shallabarger",
                Birthdate = DateTime.Parse("05/18/1888")
            };

            var book = new Book { BookTitle = "Captain from Castile" };
            book.Authors.Add(samuelShallabarger);
            book.PubDate = DateTime.Parse("01/01/1945");
            book.Publisher = "Bridgeworks";
            book.Isbn = 9781882593620;
            context.Books.Add(book);

            // There will be two reviews of this book
            book = new Book { BookTitle = "Prince of Foxes" };
            book.Authors.Add(samuelShallabarger);
            book.PubDate = DateTime.Parse("01/01/1947");
            book.Isbn = 1882593642;
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
            book.PubDate = DateTime.Parse("1/1/2018");
            book.Isbn = 0802128785;
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
            book.PubDate = DateTime.Parse("1/1/1819");
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
            book.PubDate = DateTime.Parse("1/1/1937");
            book.Authors.Add(new Author { Name = "J.R.R. Tolkien" });
            review = new Review
            {
                ReviewText = "This is a classic that lives up to its reputation!",
                Reviewer = brianBird,
                ReviewDate = DateTime.Parse("09/22/2020")
            };
            book.Reviews.Add(review);
            context.Books.Add(book);

            // There will be two books by this author
            var maryDelameter = new Author { Name = "Mary Delameter" };

            // Another book and the sixth review
            book = new Book { BookTitle = "Murach's ASP.NET Core MVC, 2nd Ed." };
            book.Authors.Add(maryDelameter);
            book.Authors.Add(new Author { Name = "Joel Murach" });
            book.PubDate = DateTime.Parse("11/1/2022");
            book.Publisher = "Murach";
            book.Isbn = 1943873029;
            review = new Review
            {
                ReviewText = "This is a great book for learning ASP.NET MVC! " +
                "The second edition uses .NET 6.0 so it's quite up to date.",
                Reviewer = brianBird,
                ReviewDate = DateTime.Parse("11/1/2020")
            };
            book.Reviews.Add(review);
            context.Books.Add(book);

            // Another book, the second by this author
            book = new Book { BookTitle = "Murach's JavaScript, 2nd Ed." };
            book.Authors.Add(maryDelameter);
            book.PubDate = DateTime.Parse("9/1/2015");
            book.Publisher = "Murach";
            book.Isbn = 9781890774851;
            book.Reviews.Add(review);
            context.Books.Add(book);

            context.SaveChanges(); // stores all the books with thier related data in the DB
        }
    }
}