using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CityTrivia.Infrastructure.Services
{
    public static class SwaggerService
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            var apiDescriptionProvider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            services.AddSwaggerGen(setupAction =>
             {
                 foreach (var service in apiDescriptionProvider.ApiVersionDescriptions)
                 {
                     setupAction.SwaggerDoc($"{service.GroupName}", new()
                     {
                         Title = "City Trivia Info",
                         Version = service.ApiVersion.ToString(),
                         Description = $"Documentation for {service.ApiVersion.ToString()}"
                     });
                 }
             });
            return services;
        }

        public static WebApplication UseSwaggerInterface(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                var versions = app.DescribeApiVersions();
                foreach (var version in versions)
                {
                    setupAction.SwaggerEndpoint($"{version.GroupName}/swagger.json",
                        version.GroupName.ToUpperInvariant());
                }

            });
            return app;
        }
    }
}
