using AutoMapper;
using CityTrivia.DataAccess.Entities;
using CityTrivia.WebApi.Models;

namespace CityTrivia.WebApi.MappingProfiles
{
    public class TriviaMappingProfile : Profile
    {
        public TriviaMappingProfile()
        {
            CreateMap<Trivia, TriviaGetModel>();
            CreateMap<TriviaPostModel, Trivia>();
            CreateMap<Trivia, TriviaPostModel>();
        }
    }
}
