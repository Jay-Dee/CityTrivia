using CityTrivia.WebApi.Entities;

namespace CityTrivia.WebApi.Services {
    public interface ICitiesRepository {

        Task<IEnumerable<City>> GetCitiesAsync();

        Task<City?> GetCity(int cityId);

        Task<City> AddCity(City city);

        void RemoveCity(City city);

        Task<bool> SaveChangesAsync();
    }
}
