using ProductReviewApp.Models;

namespace ProductReviewApp.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProductById(int id);
        Product GetProductByName(string name);
        decimal GetProductRating(int id);
        bool IsProductAvailable(int id);
    }
}
