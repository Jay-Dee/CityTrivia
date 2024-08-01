using AutoMapper;
using CityTrivia.DataAccess.Entities;
using CityTrivia.WebApi.Models;

namespace CityTrivia.WebApi.MappingProfiles {
    public class CityMappingProfile : Profile{
        public CityMappingProfile() {
            CreateMap<City, CityGetModel>();
            CreateMap<CityPostModel, City>();
            CreateMap<City, CityPostModel> ();
        }
    }
}
