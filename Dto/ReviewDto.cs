using System.ComponentModel.DataAnnotations;

namespace ProductReviewApp.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        [Range(1, 10, ErrorMessage = "Rating should be between 1-10")]
        public int Rating { get; set; }
        public DateTime ReviewedOn { get; set; }
    }
}
