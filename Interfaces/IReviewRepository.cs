using ProductReviewApp.Models;

namespace ProductReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetAllReviews();
        Review GetReviewById(int id);
        ICollection<Review> GetProductReviews(int productId);
        bool ReviewExists(int id);
    }
}
