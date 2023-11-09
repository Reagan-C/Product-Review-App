using Microsoft.EntityFrameworkCore;
using ProductReviewApp.Data;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Review> GetAllReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public Reviewer GetReviewer(int id)
        {
            return _context.Reviewers.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.OrderBy(r => r.Id).ToList();
        }

        public bool ReviewerExists(int id)
        {
            return _context.Reviewers.Any(r  => r.Id == id);
        }
    }
}
