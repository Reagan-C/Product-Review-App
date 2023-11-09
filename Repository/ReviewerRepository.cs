using ProductReviewApp.Data;
using ProductReviewApp.Interfaces;

namespace ProductReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }
        public bool ReviewerExists(int id)
        {
            return _context.Reviewers.Any(r  => r.Id == id);
        }
    }
}
