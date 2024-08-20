using Microsoft.Extensions.DependencyInjection;
using CityTrivia.DataAccess.Contexts;
using CityTrivia.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CityTrivia.DataAccess
{
    public static class PersistenceService
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ICitiesTriviaDbContext, CitiesTriviaDbContext>
                (options => options.UseSqlite(connectionString, 
                                                b => b.MigrationsAssembly("CityTrivia.WebApi")));
            services.AddScoped<ICitiesRepository, CitiesRepository>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
