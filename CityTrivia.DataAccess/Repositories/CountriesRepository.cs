using CityTrivia.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTrivia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityTrivia.DataAccess.Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly ICitiesTriviaDbContext _cityTriviaDbContext;

        public CountriesRepository(ICitiesTriviaDbContext cityTriviaDbContext) 
        {
            _cityTriviaDbContext = cityTriviaDbContext;
        }

        public void Add(Country entity)
        {
            _cityTriviaDbContext.Countries.Add(entity);
        }

        public async Task<IEnumerable<Country>> GetAllAsync(string? nameToFilter, int skipCount, int takeCount)
        {
            var entitiesAsQueryable = _cityTriviaDbContext.Countries.AsQueryable();
            if (!string.IsNullOrEmpty(nameToFilter))
            {
                entitiesAsQueryable = entitiesAsQueryable.Where(c => c.Name.Contains(nameToFilter));
            }
            return await entitiesAsQueryable.OrderBy(c => c.Name).Skip(skipCount).Take(takeCount).ToListAsync();
        }

        public async Task<Country?> GetAsync(int entityId)
        {
            return await _cityTriviaDbContext
                            .Countries
                            .AsQueryable<Country>()
                            .Where(c => c.Id == entityId)
                            .FirstOrDefaultAsync();
        }

        public void Remove(Country entity)
        {
            _cityTriviaDbContext.Countries.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _cityTriviaDbContext.SaveChangesAsync() >= 0;
        }

        public void Update(Country entity)
        {
            _cityTriviaDbContext.Countries.Update(entity);
        }
    }
}
