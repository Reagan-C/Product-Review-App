using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductReviewApp.Dto;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;

        public ProductController(IProductRepository productRepository, IMapper mapper, IReviewRepository reviewRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts() 
        { 
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int id) 
        { 
            if (!_productRepository.IsProductAvailable(id))
                return NotFound();

            var product = _mapper.Map<ProductDto>(_productRepository.GetProductById(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(product);
        }

        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type =typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetProductRating(int id) 
        {
            if (!_productRepository.IsProductAvailable(id))
                return NotFound();

            var AverageRating = _productRepository.GetProductRating(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(AverageRating);
        }

        [HttpGet("reviews/{productId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews(int productId) 
        { 
            if (!_productRepository.IsProductAvailable(productId))
                return NotFound();

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetProductReviews(productId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

    }
}
