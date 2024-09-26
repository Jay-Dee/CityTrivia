using CityTrivia.DataAccess.Contexts;
using CityTrivia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityTrivia.DataAccess.Repositories
{
    public class TriviasRepository : ITriviasRepository
    {
        private readonly ICitiesTriviaDbContext _cityTriviaDbContext;

        public TriviasRepository(ICitiesTriviaDbContext cityTriviaDbContext)
        {
            _cityTriviaDbContext = cityTriviaDbContext;
        }

        public void Add(Trivia entity)
        {
            _cityTriviaDbContext.Trivias.Add(entity);
        }

        public async Task<IEnumerable<Trivia>> GetAllAsync(string? nameToFilter, int skipCount, int takeCount)
        {
            var entitiesAsQueryable = _cityTriviaDbContext.Trivias.AsQueryable();
            if (!string.IsNullOrEmpty(nameToFilter))
            {
                entitiesAsQueryable = entitiesAsQueryable.Where(c => c.Information.Contains(nameToFilter));
            }
            return await entitiesAsQueryable.OrderBy(c => c.Information).Skip(skipCount).Take(takeCount).ToListAsync();
        }

        public async Task<Trivia?> GetAsync(int entityId)
        {
            return await _cityTriviaDbContext
                            .Trivias
                            .AsQueryable<Trivia>()
                            .Where(c => c.Id == entityId)
                            .FirstOrDefaultAsync();
        }

        public void Remove(Trivia entity)
        {
            _cityTriviaDbContext.Trivias.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _cityTriviaDbContext.SaveChangesAsync() >= 0;
        }

        public void Update(Trivia entity)
        {
            _cityTriviaDbContext.Trivias.Update(entity);
        }
    }
}
