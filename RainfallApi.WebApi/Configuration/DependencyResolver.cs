using RainfallApi.Application.Interface;
using RainfallApi.Application.Service;
using RainfallApi.DataAccess.Interface;
using RainfallApi.DataAccess.Repository;

namespace RainfallApi.WebApi.Configuration
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class DependencyResolver
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRainfallReadingService, RainfallReadingService>();
            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRainfallReadingRepository, RainfallReadingRepository>();
            return services;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
