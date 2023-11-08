﻿namespace ProductReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating  { get; set; } 
        public DateTime ReviewedOn { get; set; }    
        public Reviewer Reviewer { get; set; }
        public Product Product { get; set; }
    }
}
