using CityTrivia.WebApi.DbContext;
using CityTrivia.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityTrivia.WebApi.Services {
    public class CitiesRepository : ICitiesRepository {
        private readonly ICitiesTriviaDbContext _cityTriviaDbContext;

        public CitiesRepository(ICitiesTriviaDbContext cityTriviaDbContext) {
            _cityTriviaDbContext = cityTriviaDbContext;
        }

        public async Task<City> AddCity(City city) {
            await _cityTriviaDbContext.Cities.AddAsync(city);
            return city;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync() {
            var citiesAsQueryable = _cityTriviaDbContext.Cities.AsQueryable<City>();
            return await citiesAsQueryable.OrderBy(c => c.Name).ToListAsync();
        }

        public Task<City?> GetCity(int cityId) {
            var city = _cityTriviaDbContext.Cities.AsQueryable<City>().Where(c => c.Id == cityId).FirstOrDefault();
            return Task.FromResult(city);
        }

        public void RemoveCity(City city) {
            _cityTriviaDbContext.Cities.Remove(city);
        }

        public async Task<bool> SaveChangesAsync() {
            return(await _cityTriviaDbContext.SaveChangesAsync() >= 0);
        }
    }
}
