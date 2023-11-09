using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductReviewApp.Dto;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IProductRepository productRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetAllReviews() 
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetAllReviews());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("product/{productId}")]
        [ProducesResponseType(200, Type =typeof(IEnumerable<Review>))]
        public IActionResult GetProductReviews(int productId) 
        {
            if (!_productRepository.IsProductAvailable(productId))
                return NotFound("Product does not exist in our records");

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetProductReviews(productId));

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        public IActionResult GetReviewByID(int id) 
        {
            if (!_reviewRepository.ReviewExists(id))
                return NotFound("No review with Id " +  id + " in our records");

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReviewById(id));

            return Ok(review);
        }
    }
}
