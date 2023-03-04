﻿using System;

namespace BookReviews.Models
{
    public class Book
    {
        // Backinig fields for properties
        private List<Author> authorList = new List<Author>();
        private List<Review> reviewList = new List<Review>();

        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public ulong Isbn { get; set; }
        public string? Publisher { get; set; }
        public DateTime PubDate { get; set; }

        public List<Author> Authors 
        { get { return authorList; } }

        public List<Review> Reviews  
        { get { return reviewList; } }
    }
}
