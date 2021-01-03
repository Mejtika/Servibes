using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.Appointments.Application.Events.Appointments;
using Servibes.Availability.Application;
using Servibes.Availability.Application.Events.External.Appointments.AppointmentRejected;
using Servibes.Availability.Application.Events.External.EmployeeAdded;
using Servibes.Availability.Application.Events.External.RegistrationCompleted;
using Servibes.Availability.Application.Events.External.TimeReservations.TimeReservationCanceled;
using Servibes.Availability.Application.Events.External.TimeReservations.TimeReservationFinished;
using Servibes.Availability.Application.ModuleClients;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Employees;
using Servibes.Availability.Infrastructure.Domain.Companies;
using Servibes.Availability.Infrastructure.Domain.Employees;
using Servibes.Availability.Infrastructure.ModuleClients;
using Servibes.Shared;
using Servibes.Shared.Communication.Events;

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
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<IAvailabilityUnitOfWork, AvailabilityUnitOfWork>();
            services.AddTransient<IReservationClient, ReservationClient>();
            services.AddSingleton<IEventMapper, EventMapper>();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseModuleRequests()
                .Subscribe<EmployeeAddedEvent>()
                .Subscribe<RegistrationCompletedEvent>()
                .Subscribe<AppointmentRejectedEvent>()
                .Subscribe<AppointmentCanceledEvent>()
                .Subscribe<AppointmentFinishedEvent>()
                .Subscribe<TimeReservationCanceledEvent>()
                .Subscribe<TimeReservationFinishedEvent>();

            return app;
        }
    }
}
