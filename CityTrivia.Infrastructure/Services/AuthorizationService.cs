﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace CityTrivia.Infrastructure.Services
{
    public static class AuthorizationService
    {
        public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
        {
            services.AddAuthorization(config =>
             {
                 config.AddPolicy("AuthZPolicy", policyBuilder =>
                     policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement() { RequiredScopesConfigurationKey = $"AzureAd:Scopes" }));
             });

            return services;
        }

    }
}
