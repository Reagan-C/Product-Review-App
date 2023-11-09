using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductReviewApp.Dto;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        public IActionResult GetReviewer(int id) 
        {
            if (!_reviewerRepository.ReviewerExists(id))
                return NotFound("Reviewer not found");

            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }

        [HttpGet("reviews/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviewsByReviewer(int reviewerId) 
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound("Invalid ID");

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetAllReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet]
        [ProducesResponseType(200, Type =typeof(IEnumerable<Reviewer>))]
        public IActionResult GetAllReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }
    }
}
