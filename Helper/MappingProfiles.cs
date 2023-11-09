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
            CreateMap<CategoryDto, Category>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<ManufacturerDto, Manufacturer>();
            CreateMap<Review, ReviewDto>();

            
        }
    }
}
