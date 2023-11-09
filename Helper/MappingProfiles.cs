using AutoMapper;
using ProductReviewApp.Dto;
using ProductReviewApp.Models;

namespace ProductReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<Review, ReviewDto>();
        }
    }
}
