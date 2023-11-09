using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductReviewApp.Dto;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories() 
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId) 
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("{categoryId}/products")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(400)]
        public IActionResult GetProductsByCategoryId(int categoryId) 
        {
            if (!_categoryRepository.CategoryExists(categoryId)) 
                return NotFound();

            var products = _mapper.Map<List<ProductDto>>(_categoryRepository.GetProductsByCategory(categoryId));
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult CreateCategory([FromBody]CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest(ModelState);

            var category = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryDto.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryDto);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving category");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Successfully created");
        }
    }
}
