using ProductReviewApp.Models;

namespace ProductReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountryById(int id);
        Country GetCountryByName(string name);
        Country GetCountryByReviewer(int reviewerId);
        ICollection<Reviewer> GetReviewersFromACountry(int countryId);
        bool CountryExistsByName(string name);
        bool CountryExistsById(int countryId);
        bool CreateCountry(Country country);
        bool Save();
    }
}
