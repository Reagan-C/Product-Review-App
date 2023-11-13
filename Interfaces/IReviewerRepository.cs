using ProductReviewApp.Models;

namespace ProductReviewApp.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        ICollection<Review> GetAllReviewsByReviewer(int reviewerId);
        bool ReviewerExists(int  id);
        bool CreateReviewer(Reviewer reviewer);
        bool Save();
        bool UpdateReviewer(Reviewer reviewer);
    }
}
