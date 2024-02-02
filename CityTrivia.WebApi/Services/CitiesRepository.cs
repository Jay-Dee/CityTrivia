using CityTrivia.WebApi.DbContext;
using CityTrivia.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityTrivia.WebApi.Services {
    public class CitiesRepository : ICitiesRepository {
        
        private readonly ICitiesTriviaDbContext _cityTriviaDbContext;


        public CitiesRepository(ICitiesTriviaDbContext cityTriviaDbContext) {
            _cityTriviaDbContext = cityTriviaDbContext;
        }

        public void AddCity(City city) {
            _cityTriviaDbContext.Cities.Add(city);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync(string? nameToFilter, int skipCount, int takeCount) {
            var citiesAsQueryable = _cityTriviaDbContext.Cities.AsQueryable();
            if (!string.IsNullOrEmpty(nameToFilter)) {
                citiesAsQueryable = citiesAsQueryable.Where(c => c.Name.Contains(nameToFilter));
            }
            return await citiesAsQueryable.OrderBy(c => c.Name).Skip(skipCount).Take(takeCount).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId) {
            return await _cityTriviaDbContext.Cities.AsQueryable<City>().Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }
        public void UpdateCity(City city) {
            _cityTriviaDbContext.Cities.Update(city);
        }

        public void RemoveCity(City city) {
            _cityTriviaDbContext.Cities.Remove(city);
        }

        public async Task<bool> SaveChangesAsync() {
            return(await _cityTriviaDbContext.SaveChangesAsync() >= 0);
        }
    }
}
