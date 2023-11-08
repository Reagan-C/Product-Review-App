namespace ProductReviewApp.Models
{
    public class Reviewer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; init; }
        public ICollection<Review> Reviews { get; set; }
        public Country Country { get; set; }
    }
}
