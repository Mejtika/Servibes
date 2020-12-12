using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.Availability.Application;
using Servibes.Availability.Application.Events.External.AppointmentCreated;
using Servibes.Availability.Application.Events.External.EmployeeAdded;
using Servibes.Availability.Application.Events.External.RegistrationCompleted;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Employees;
using Servibes.Availability.Infrastructure.Domain.Companies;
using Servibes.Availability.Infrastructure.Domain.Employees;
using Servibes.Shared;

namespace Servibes.Availability.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AvailabilityContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsHistoryTable("__AvailabilityMigrationsHistory", "availability");
                    });
            });

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IAvailabilityUnitOfWork, AvailabilityUnitOfWork>();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseModuleRequests()
                .Subscribe<EmployeeAddedEvent>()
                .Subscribe<RegistrationCompletedEvent>()
                .Subscribe<AppointmentCreatedEvent>();

            return app;
        }
    }
}
