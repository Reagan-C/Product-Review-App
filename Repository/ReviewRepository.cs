using ProductReviewApp.Data;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IProductRepository _productRepository;

        public ReviewRepository(DataContext  dataContext, IReviewerRepository reviewerRepository, 
            IProductRepository productRepository) 
        {
            _context = dataContext;
            _reviewerRepository = reviewerRepository;
            _productRepository = productRepository;
        }

        public bool AddReview(int reviewerId, int productId, Review review)
        {
            var reviewer = _reviewerRepository.GetReviewer(reviewerId);
            var product = _productRepository.GetProductById(productId);

            if (reviewer == null | product == null)
                return false;

            review.Product = product;
            review.Reviewer = reviewer;
            review.ReviewedOn = DateTime.Now;

            _context.Add(review);
            return Save();
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

        public bool Save()
        {
            var savedReview = _context.SaveChanges();
            return savedReview > 0 ? true : false;
        }
    }
}
