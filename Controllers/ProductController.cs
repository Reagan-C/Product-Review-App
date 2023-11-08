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

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
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

    }
}
