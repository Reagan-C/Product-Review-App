using ProductReviewApp.Data;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public Product GetProductByName(string name)
        {
            return _context.Products.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetProductRating(int id)
        {
            var reviews = _context.Reviews.Where(r => r.Product.Id == id);

            if (reviews.Count() <= 0) 
                return 0;

            return (decimal)reviews.Sum(r => r.Rating) / reviews.Count();
        }

        public  ICollection<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }

        public bool IsProductAvailable(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
