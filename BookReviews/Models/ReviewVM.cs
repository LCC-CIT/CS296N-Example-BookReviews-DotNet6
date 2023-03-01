﻿using System;

namespace BookReviews.Models
{
    public class ReviewVM
    {
        public int ReviewId { get; set; }
        public AppUser Reviewer { get; set; }
        public Book Book { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public ICollection<Comment> Comments { get; set; }  // Compositioin--FK in comment
    }
}