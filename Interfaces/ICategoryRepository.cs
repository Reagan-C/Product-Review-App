using ProductReviewApp.Models;

namespace ProductReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Product> GetProductsByCategory(int id);
        bool CategoryExists(int id);
    }
}
