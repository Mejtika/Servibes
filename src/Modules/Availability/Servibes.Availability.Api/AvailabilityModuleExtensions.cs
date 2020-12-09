﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.Availability.Infrastructure;

namespace Servibes.Availability.Api
{
    public static class AvailabilityModuleExtensions
    {
        public static IServiceCollection AddAvailabilityModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddInfrastructure(configuration);
        }

        public static IApplicationBuilder UseAvailabilityModule(this IApplicationBuilder app)
        {
            return app
                .UseInfrastructure();
        }
    }
}
