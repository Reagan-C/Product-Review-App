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
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, IMapper mapper, 
            IReviewRepository reviewRepository, IManufacturerRepository manufacturerRepository, 
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _manufacturerRepository = manufacturerRepository;
            _categoryRepository = categoryRepository;

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

        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult AddProduct([FromQuery]int categoryId, [FromQuery]int manufacturerId,
            [FromBody]ProductDto productDto)
        {
            if (productDto == null ) 
                return BadRequest(ModelState);

            var checkProduct = _productRepository.GetProducts()
                .Where(p => p.Name.Trim().ToLower() == productDto.Name.Trim().ToLower())
                .FirstOrDefault();

            if (checkProduct != null)
            {
                ModelState.AddModelError("", "A Product with this name already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var saveProduct = _mapper.Map<Product>(productDto);
            

            if (!_productRepository.AddProduct(categoryId, manufacturerId, saveProduct))
            {
                ModelState.AddModelError("", "Problem encountered while saving product, please check input parameters");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Product saved");
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(204)]
        public IActionResult UpdateProduct(int productId, [FromBody]ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest(ModelState);

            if (productId != productDto.Id) 
                return BadRequest("IDs do not match");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.IsProductAvailable(productId))
                return NotFound("Product doesnt exist in the records");

            var product = _mapper.Map<Product>(productDto);
            if (!_productRepository.UpdateProduct(product))
                return StatusCode(500, "Problem encountered while saving product");

            return NoContent();
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteProduct(int productId)
        {
            if (!_productRepository.IsProductAvailable(productId))
                return NotFound("Invalid product ID");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviews = _reviewRepository.GetProductReviews(productId);
            var productToRemove = _productRepository.GetProductById(productId);

            if (!_reviewRepository.DeleteReviews(reviews.ToList()))
                return StatusCode(422, "something went wrong while deleting reviews");

            if (!_productRepository.DeleteProduct(productToRemove))
                return StatusCode(422, "something went wrong");

            return NoContent();
        }

    }
}