using ProductReviewApp.Data;
using ProductReviewApp.Interfaces;
using ProductReviewApp.Models;

namespace ProductReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CountryExistsById(int countryId)
        {
            return _context.Countries.Any(c => c.Id == countryId);
        }

        public bool CountryExistsByName(string name)
        {
            return _context.Countries.Any(c => c.Name == name);
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(c => c.Id).ToList();
        }

        public Country GetCountryByName(string name)
        {
            return _context.Countries.Where(c => c.Name == name).FirstOrDefault();
        }

        public Country GetCountryById(int id)
        {
            return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.Id == reviewerId)
                .Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewersFromACountry(int countryId)
        {
            return _context.Reviewers.Where(r => r.Country.Id == countryId).ToList();
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public bool Save()
        {
            var savedCountry = _context.SaveChanges();
            return savedCountry > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Save();
        }
    }
}
