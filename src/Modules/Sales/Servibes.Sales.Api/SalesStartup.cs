using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.Sales.Api.Events.External.AppointmentFinished;
using Servibes.Sales.Api.Events.External.EmployeeAdded;
using Servibes.Sales.Api.Events.External.NewClientRegistered;
using Servibes.Sales.Api.Events.External.RegistrationCompleted;
using Servibes.Sales.Api.ModuleClients;
using Servibes.Shared;

namespace Servibes.Sales.Api
{
    public static class SalesStartup
    {
        public static IServiceCollection AddSalesModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddDbContext<SalesContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsHistoryTable("__SalesMigrationsHistory", "sales");
                    });
            });

            services.AddTransient<AuthorizationClient>();

            return services;
        }

        public static IApplicationBuilder UseSalesModule(this IApplicationBuilder app)
        {
            app.UseModuleRequests()
                .Subscribe<AppointmentFinishedEvent>()
                .Subscribe<EmployeeAddedEvent>()
                .Subscribe<NewClientRegisteredEvent>()
                .Subscribe<RegistrationCompletedEvent>();

            return app;
        }
    }
}
