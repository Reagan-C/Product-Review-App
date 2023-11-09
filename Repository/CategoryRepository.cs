using ProductReviewApp.Data;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context) 
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Product> GetProductsByCategory(int id)
        {
            return _context.ProductCategories.Where(pc => pc.CategoryId == id)
                .Select(p => p.Product).ToList();
        }

        public bool Save()
        {
            var savedCategory = _context.SaveChanges();
            return savedCategory > 0 ? true : false;
        }
    }
}
