﻿using AutoMapper;
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
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, 
            IProductRepository productRepository, IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _reviewerRepository = reviewerRepository;
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

        [HttpPost]
        [ProducesResponseType (201)]
        public IActionResult AddReview([FromQuery]int reviewerId, [FromQuery]int productId,
            [FromBody]ReviewDto reviewDto)
        {
            if (reviewDto == null) 
                return BadRequest(ModelState);

            var checkedReview = _reviewRepository.GetAllReviews()
                .Where(pr => pr.Title.Trim().ToLower() == reviewDto.Title.Trim().ToLower())
                .FirstOrDefault();

            if (checkedReview != null)
            {
                    ModelState.AddModelError("", "Review already exists");
                    return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var savedReview = _mapper.Map<Review>(reviewDto);

            if (!_reviewRepository.AddReview(reviewerId, productId, savedReview))
            {
                ModelState.AddModelError("", "Problem encountered while saving review");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Review added");
        }
    }
}
