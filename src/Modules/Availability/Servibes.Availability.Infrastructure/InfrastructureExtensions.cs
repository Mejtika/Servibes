using System;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.Availability.Application.Events.External.EmployeeAdded;
using Servibes.Availability.Application.Events.External.RegistrationCompleted;
using Servibes.Shared;

namespace Servibes.Availability.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddDbContext<AvailabilityContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsHistoryTable("__AvailabilityMigrationsHistory", "availability");
                    });
            });

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseModuleRequests()
                .Subscribe<EmployeeAddedEvent>()
                .Subscribe<RegistrationCompletedEvent>();

            return app;
        }
    }
}
