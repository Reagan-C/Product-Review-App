using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductReviewApp.Dto;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IMapper _mapper;

        public ManufacturerController(IManufacturerRepository manufacturerRepository, IMapper mapper)
        {
            _manufacturerRepository = manufacturerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Manufacturer>))]
        public IActionResult GetAllManufacturers() 
        {
            var manufacturers = _mapper.Map<List<ManufacturerDto>>(_manufacturerRepository.GetManufacturers());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Manufacturer))]
        [ProducesResponseType(400)]
        public IActionResult GetManufacturer(int id) 
        {
            if (!_manufacturerRepository.ManufacturerExists(id))
                return NotFound();

            var manufacturer = _mapper.Map<ManufacturerDto>(_manufacturerRepository.GetManufacturerById(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(manufacturer);
        }

        [HttpGet("products/{manufacturerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(400)]
        public IActionResult GetProductsByManufacturerId(int manufacturerId) 
        {
            if (!_manufacturerRepository.ManufacturerExists(manufacturerId))
                return NotFound("No such manufacturer in our records");

            var products = _mapper.Map<List<ProductDto>>(_manufacturerRepository.GetProductsByManufacturer(manufacturerId));

            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(products);
        }
    }
}
