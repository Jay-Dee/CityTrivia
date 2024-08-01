using CityTrivia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityTrivia.DataAccess.Contexts
{
    public interface ICitiesTriviaDbContext
    {
        DbSet<City> Cities { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class CitiesTriviaDbContext : DbContext, ICitiesTriviaDbContext
    {

        public CitiesTriviaDbContext(DbContextOptions<CitiesTriviaDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; } = null!;
    }
}
