using ProductReviewApp.Data;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Repository
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DataContext _context;

        public ManufacturerRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public bool AddManufacturer(Manufacturer manufacturer)
        {
            _context.Add(manufacturer);
            return Save();
        }

        public Manufacturer GetManufacturerById(int manufacturerId)
        {
            return _context.Manufacturers.Where(m => m.Id == manufacturerId).FirstOrDefault();
        }

        public ICollection<Manufacturer> GetManufacturers()
        {
            return _context.Manufacturers.OrderBy(m => m.Id).ToList();
        }

        public ICollection<Product> GetProductsByManufacturer(int manufacturerId)
        {
            return _context.Products.Where(p => p.Manufacturer.Id == manufacturerId).ToList();
        }

        public bool ManufacturerExists(int manufacturerId)
        {
            return _context.Manufacturers.Any(m => m.Id == manufacturerId);
        }

        public bool Save()
        {
            var savedManufacturer = _context.SaveChanges();
            return savedManufacturer > 0 ? true : false;
        }

        public bool UpdateManufacturer(Manufacturer manufacturer)
        {
            _context.Update(manufacturer);
            return Save();
        }
    }
}
