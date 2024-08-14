using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesTrivia.UseCases
{
    public static class MediatRService
    {
        public static IServiceCollection AddMediatRService(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            return services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}
