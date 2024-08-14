using Microsoft.Extensions.DependencyInjection;

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
