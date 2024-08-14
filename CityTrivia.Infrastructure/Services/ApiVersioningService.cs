using Microsoft.Extensions.DependencyInjection;

namespace CityTrivia.Infrastructure.Services
{
    public static class ApiVersioningService
    {
        public static IServiceCollection AddApiVersioningService(this IServiceCollection services)
        {
            services
                .AddApiVersioning(setupAction =>
                    {
                        setupAction.ReportApiVersions = true;
                        setupAction.AssumeDefaultVersionWhenUnspecified = true;
                        setupAction.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
                    })
                .AddMvc()
                .AddApiExplorer(setupAction => setupAction.SubstituteApiVersionInUrl = true);
            return services;
        }
    }
}
