using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductReviewApp.Dto;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;
using ProductReviewApp.Repository;

namespace ProductReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper,
            ICountryRepository countryRepository, IReviewRepository reviewRepository)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
            _reviewRepository=reviewRepository;
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

        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult AddReviewer([FromQuery]int countryId, [FromBody]ReviewerDto reviewerDto)
        {
            if (reviewerDto == null)
                return BadRequest(ModelState);

            var reviewer = _reviewerRepository.GetReviewers()
                .Where(r => r.LastName.Trim().ToLower() == reviewerDto.LastName.Trim().ToLower())
                .FirstOrDefault();

            if (reviewer != null)
            {
                ModelState.AddModelError("", "Reviewer already exists");
                return StatusCode(422, ModelState);
            }

            var country = _countryRepository.GetCountryById(countryId);
            if (country == null)
                return BadRequest("Invalid country ID");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(reviewerDto);
            reviewerMap.Country = country;

            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Problem encountered while saving reviewer details");
                return StatusCode(500, ModelState);
            }
            
            return StatusCode(201, "Reviewer Added");
        }

        [HttpPut("{reviewerId}")]
        [ProducesResponseType(204)]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody]ReviewerDto reviewerDto)
        {
            if (reviewerDto == null)
                return BadRequest(ModelState);

            if (reviewerId != reviewerDto.Id)
                return BadRequest("Ids do not match");

            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound("Reviewer not found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewer = _mapper.Map<Reviewer>(reviewerDto);

            if (!_reviewerRepository.UpdateReviewer(reviewer))
                return StatusCode(500, "Problem encountered while saving reviewer details");

            return NoContent();
        }

        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound("Invalid reviewer ID");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviews = _reviewerRepository.GetAllReviewsByReviewer(reviewerId);
            var reviewer = _reviewerRepository.GetReviewer(reviewerId);

            if (!_reviewRepository.DeleteReviews(reviews.ToList()))
                return StatusCode(422, "something went wrong deleting reviews");

            if (!_reviewerRepository.DeleteReviewer(reviewer))
                return StatusCode(422, "something went wrong");

            return NoContent();
        }
    }
}
