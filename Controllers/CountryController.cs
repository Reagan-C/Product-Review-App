using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductReviewApp.Dto;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly IReviewerRepository _reviewerRepository;

        public CountryController(ICountryRepository countryRepository, IMapper mapper, IReviewerRepository reviewerRepository)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetAllCountries() 
        { 
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            if (!ModelState.IsValid) 
                return BadRequest();
            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryById(int countryId) 
        {
            if (!_countryRepository.CountryExistsById(countryId))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryById(countryId));
            if (!ModelState.IsValid) 
                return BadRequest();
            
            return Ok(country);
        }

        [HttpGet("name/{countryName}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryByName(string countryName)
        { 
            if (!_countryRepository.CountryExistsByName(countryName))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByName(countryName));
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }

        [HttpGet("reviewer/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountryOfReviewer(int reviewerId) 
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryRepository
                .GetCountryByReviewer(reviewerId));
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }

        [HttpGet("{countryId}/reviewers")]
        [ProducesResponseType(200, Type =typeof(IEnumerable<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewersByCountryId(int countryId) 
        {
            if (!_countryRepository.CountryExistsById(countryId))
                return NotFound();

            var reviewers = _mapper.Map<List<ReviewerDto>>(_countryRepository.GetReviewersFromACountry(countryId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult AddCountry([FromBody] CountryDto countryDto)
        {
            if (countryDto == null)
                return BadRequest(ModelState);

            var country = _countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countryDto.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(countryDto);

            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wront while saving country details");
                return StatusCode(500, ModelState);
            }
            return StatusCode(201, "Successfully added");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(204)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto countryDto)
        {
            if (countryDto == null)
                return BadRequest(ModelState);

            if (countryId != countryDto.Id)
                return BadRequest("IDs does not match");

            if (!_countryRepository.CountryExistsById(countryId))
                return NotFound("Country with an ID " + countryId + " doesnt exist in our records");

            if (!ModelState.IsValid)
                return BadRequest();

            var country = _mapper.Map<Country>(countryDto);

            if (!_countryRepository.UpdateCountry(country))
                return StatusCode(422, "Problem encountered while updating country");

            return NoContent();
        }
    }
}
