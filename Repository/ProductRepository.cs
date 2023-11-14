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

        public bool AddProduct(int categoryId, int manufacturerId, Product product)
        {
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            var manufacturer = _context.Manufacturers.Where(m => m.Id == manufacturerId).FirstOrDefault();

            if (manufacturer == null || category == null)
                return false;

            var productCategory = new ProductCategory()
            {
                Product = product,
                Category = category
            };

            product.Manufacturer = manufacturer;

            _context.Add(product);
            _context.Add(productCategory);
            return Save();
        }

        public bool DeleteProduct(Product product)
        {
            _context.Remove(product);
            return Save(); 
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

        public bool Save()
        {
            var savedProduct = _context.SaveChanges();
            return savedProduct > 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}
