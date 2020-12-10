using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.Availability.Application;
using Servibes.Availability.Infrastructure;

namespace Servibes.Availability.Api
{
    public static class AvailabilityModuleExtensions
    {
        public static IServiceCollection AddAvailabilityModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddApplication()
                .AddInfrastructure(configuration);
        }

        public static IApplicationBuilder UseAvailabilityModule(this IApplicationBuilder app)
        {
            return app
                .UseApplication()
                .UseInfrastructure();
        }
    }
}
