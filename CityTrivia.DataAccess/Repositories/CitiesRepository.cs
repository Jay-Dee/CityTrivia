﻿using CityTrivia.DataAccess.Contexts;
using CityTrivia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityTrivia.DataAccess.Repositories
{
    public class CitiesRepository : ICitiesRepository
    {
        private readonly ICitiesTriviaDbContext _cityTriviaDbContext;


        public CitiesRepository(ICitiesTriviaDbContext cityTriviaDbContext)
        {
            _cityTriviaDbContext = cityTriviaDbContext;
        }

        public void Add(City city)
        {
            _cityTriviaDbContext.Cities.Add(city);
        }

        public async Task<IEnumerable<City>> GetAllAsync(string? nameToFilter, int skipCount, int takeCount)
        {
            var citiesAsQueryable = _cityTriviaDbContext.Cities.AsQueryable();
            if (!string.IsNullOrEmpty(nameToFilter))
            {
                citiesAsQueryable = citiesAsQueryable.Where(c => c.Name.Contains(nameToFilter));
            }
            return await citiesAsQueryable.Include(c => c.Country).OrderBy(c => c.Name).Skip(skipCount).Take(takeCount).ToListAsync();
        }

        public async Task<City?> GetAsync(int cityId)
        {
            return await _cityTriviaDbContext.Cities.AsQueryable<City>().Where(c => c.Id == cityId).Include(c => c.Country).FirstOrDefaultAsync();
        }
        public void Update(City city)
        {
            _cityTriviaDbContext.Cities.Update(city);
        }

        public void Remove(City city)
        {
            _cityTriviaDbContext.Cities.Remove(city);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _cityTriviaDbContext.SaveChangesAsync() >= 0;
        }
    }
}
