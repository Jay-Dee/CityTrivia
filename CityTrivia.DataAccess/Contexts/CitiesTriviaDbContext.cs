using CityTrivia.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityTrivia.DataAccess.Contexts
{
    public interface ICitiesTriviaDbContext
    {
        DbSet<City> Cities { get; set; }

        DbSet<Country> Countries { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class CitiesTriviaDbContext : DbContext, ICitiesTriviaDbContext
    {

        public CitiesTriviaDbContext(DbContextOptions<CitiesTriviaDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; } = null!;

        public DbSet<Country> Countries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(new Country() { Id = 1, Name = "Unknown" });
        }

    }
}
