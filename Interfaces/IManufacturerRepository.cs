using ProductReviewApp.Models;

namespace ProductReviewApp.Interfaces
{
    public interface IManufacturerRepository
    {
        ICollection<Manufacturer> GetManufacturers();
        Manufacturer GetManufacturerById(int manufacturerId);
        ICollection<Product> GetProductsByManufacturer(int manufacturerId);
        bool ManufacturerExists(int manufacturerId);
    }
}
