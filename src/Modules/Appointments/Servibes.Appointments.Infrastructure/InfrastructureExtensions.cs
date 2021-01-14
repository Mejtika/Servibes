using System.Reflection;
using Coravel;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.Appointments.Application;
using Servibes.Appointments.Application.Events.External.ClientInformationUpdated;
using Servibes.Appointments.Application.Events.External.NewClientRegistered;
using Servibes.Appointments.Application.Events.External.RegistrationCompleted;
using Servibes.Appointments.Application.Events.External.ReservationAdded;
using Servibes.Appointments.Application.Events.External.ReservationCancelled;
using Servibes.Appointments.Application.Events.External.TimeReservationAdded;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.Reservees;
using Servibes.Appointments.Core.TimeReservations;
using Servibes.Appointments.Infrastructure.Domain.Appointments;
using Servibes.Appointments.Infrastructure.Domain.Reservees;
using Servibes.Appointments.Infrastructure.Domain.TimeReservations;
using Servibes.Appointments.Infrastructure.Tasks;
using Servibes.Shared;
using Servibes.Shared.Communication.Events;

namespace Servibes.Appointments.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddDbContext<AppointmentsContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsHistoryTable("__AppointmentsMigrationsHistory", "appointments");
                    });
            });

            services.AddSingleton<IEventMapper, EventMapper>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<ITimeReservationRepository, TimeReservationRepository>();
            services.AddScoped<IAppointmentUnitOfWork, AppointmentsUnitOfWork>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScheduler();
            services.AddScoped<AppointmentFinisher>();
            services.AddScoped<TimeReservationFinisher>();
            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseModuleRequests()
                .Subscribe<ReservationAddedEvent>()
                .Subscribe<TimeReservationAddedEvent>()
                .Subscribe<NewClientRegisteredEvent>()
                .Subscribe<ClientInformationUpdatedEvent>()
                .Subscribe<RegistrationCompletedEvent>()
                .Subscribe<ReservationCancelledEvent>();

            return app;
        }
    }
}
