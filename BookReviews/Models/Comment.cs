﻿using System;
namespace BookReviews.Models
{
	public class Comment
	{
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public AppUser UserName { get; set; }

       // public int ReviewId { get; set; } // Composition (a comment is part of a review.)
    }
}
