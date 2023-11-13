using System.ComponentModel.DataAnnotations;

namespace ProductReviewApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}
    }
}
