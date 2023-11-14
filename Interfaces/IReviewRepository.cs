﻿using ProductReviewApp.Dto;
using ProductReviewApp.Models;

namespace ProductReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetAllReviews();
        Review GetReviewById(int id);
        ICollection<Review> GetProductReviews(int productId);
        bool ReviewExists(int id);
        bool AddReview(int reviewerId, int productId, Review review);
        bool Save();
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
    }
}
