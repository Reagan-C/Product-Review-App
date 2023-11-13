using System.ComponentModel.DataAnnotations;

namespace ProductReviewApp.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Range(0.5, double.MaxValue, ErrorMessage = "Please enter a valid product price")]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
