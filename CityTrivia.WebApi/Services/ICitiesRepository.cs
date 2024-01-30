using CityTrivia.WebApi.Entities;

namespace CityTrivia.WebApi.Services {
    public interface ICitiesRepository {

        Task<IEnumerable<City>> GetCitiesAsync();

        Task<City?> GetCityAsync(int cityId);

        void AddCity(City city);

        void UpdateCity(City city);

        void RemoveCity(City city);

        Task<bool> SaveChangesAsync();
    }
}
