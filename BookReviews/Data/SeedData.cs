using BookReviews.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace BookReviews.Data
{
    public class SeedData
    {
     public static void Seed(ApplicationDbContext context, IServiceProvider provider)
        {
            if (!context.Reviews.Any())  // this is to prevent duplicate data from being added
            {
                // Create some fake users
                const string SECRET_PASSWORD = "Secret!123";
                var userManager = provider.GetRequiredService<UserManager<AppUser>>();
                AppUser emmaWatson = new AppUser { UserName = "Emma Watson" };
                var result = userManager.CreateAsync(emmaWatson, SECRET_PASSWORD);
                AppUser danielRadcliffe = new AppUser { UserName = "Daniel Radcliffe" };
                result = userManager.CreateAsync(danielRadcliffe, SECRET_PASSWORD);
                AppUser brianBird = new AppUser { UserName = "Brian Bird" };
                result = userManager.CreateAsync(brianBird, SECRET_PASSWORD);


                Book book = new Book { AuthorName = "Samuel Shallabarger",
                    BookTitle = "Prince of Foxes"
                };
                context.Books.Add(book);
                context.SaveChanges();

                Review review = new Review
                {
                    Book = book,
                    ReviewText = "Great book, a must read!",
                    Reviewer = emmaWatson,
                    ReviewDate = DateTime.Parse("11/1/2020")
                };
                context.Reviews.Add(review);  // queues up the review to be added to the DB

                review = new Review
                {
                    Book = book,
                    ReviewText = "I love the clever, witty dialog",
                    Reviewer = danielRadcliffe,
                    ReviewDate = DateTime.Parse("11/15/2020")
                };
                context.Reviews.Add(review);

                // My next two reviews will be by the same user, so I will create
                // the user object once and store it so that both reviews will be
                // associated with the same entity in the DB.

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
                    Reviewer = brianBird,
                    ReviewDate = DateTime.Parse("11/1/2020")
                };
                context.Reviews.Add(review);

                context.SaveChanges(); // stores all the reviews in the DB
            
            }
        }
    }
}
