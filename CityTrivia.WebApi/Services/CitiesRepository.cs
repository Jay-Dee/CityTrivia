using CityTrivia.WebApi.DbContext;
using CityTrivia.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityTrivia.WebApi.Services {
    public class CitiesRepository : ICitiesRepository {
        private readonly ICitiesTriviaDbContext cityTriviaDbContext;

        public CitiesRepository(ICitiesTriviaDbContext cityTriviaDbContext) {
            this.cityTriviaDbContext = cityTriviaDbContext;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync() {
            var citiesAsQueryable = cityTriviaDbContext.Cities.AsQueryable<City>();
            return await citiesAsQueryable.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
