using CityTrivia.WebApi.Entities;

namespace CityTrivia.WebApi.Services {
    public interface ICitiesRepository {

        Task<IEnumerable<City>> GetCitiesAsync();
    }
}
