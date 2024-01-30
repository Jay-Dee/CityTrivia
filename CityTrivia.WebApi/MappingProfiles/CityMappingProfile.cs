using AutoMapper;
using CityTrivia.WebApi.Entities;
using CityTrivia.WebApi.Models;

namespace CityTrivia.WebApi.MappingProfiles {
    public class CityMappingProfile : Profile{
        public CityMappingProfile() {
            CreateMap<City, CityGetModel>();
            CreateMap<CityPostModel, City>();
        }
    }
}
