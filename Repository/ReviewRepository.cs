using ProductReviewApp.Data;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext  dataContext) 
        {
            _context = dataContext;
        }

        public ICollection<Review> GetAllReviews()
        {
            return _context.Reviews.OrderBy(review => review.Id).ToList();
        }

        public ICollection<Review> GetProductReviews(int productId)
        {
            return _context.Reviews.Where(r => r.Product.Id == productId).ToList();
        }

        public Review GetReviewById(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(r => r.Id == id);
        }
    }
}
