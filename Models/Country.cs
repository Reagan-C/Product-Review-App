namespace ProductReviewApp.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Reviewer> Reviewers { get; set; }
    }
}
