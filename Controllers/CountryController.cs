﻿using AutoMapper;
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
                return BadRequest();

            return Ok(reviewers);
        }

    }
}
