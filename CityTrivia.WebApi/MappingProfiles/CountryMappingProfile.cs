using AutoMapper;
using CityTrivia.DataAccess.Entities;
using CityTrivia.WebApi.Models;

namespace CityTrivia.WebApi.MappingProfiles
{
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryGetModel>();
            CreateMap<CountryPostModel, Country>();
            CreateMap<Country, CountryPostModel>();
        }
    }
}
