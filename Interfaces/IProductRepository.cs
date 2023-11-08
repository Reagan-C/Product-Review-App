using ProductReviewApp.Models;

namespace ProductReviewApp.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();

    }
}
