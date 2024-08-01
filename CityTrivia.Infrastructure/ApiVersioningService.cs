using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTrivia.Infrastructure
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
