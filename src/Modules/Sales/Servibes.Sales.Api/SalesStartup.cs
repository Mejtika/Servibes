﻿using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.Sales.Api.Events.External.AppointmentFinished;
using Servibes.Sales.Api.Events.External.ClientInformationUpdated;
using Servibes.Sales.Api.Events.External.EmployeeAdded;
using Servibes.Sales.Api.Events.External.EmployeeDeleted;
using Servibes.Sales.Api.Events.External.NewClientRegistered;
using Servibes.Sales.Api.Events.External.RegistrationCompleted;
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

            return services;
        }

        public static IApplicationBuilder UseSalesModule(this IApplicationBuilder app)
        {
            app.UseModuleRequests()
                .Subscribe<AppointmentFinishedEvent>()
                .Subscribe<EmployeeAddedEvent>()
                .Subscribe<EmployeeDeletedEvent>()
                .Subscribe<NewClientRegisteredEvent>()
                .Subscribe<ClientInformationUpdatedEvent>()
                .Subscribe<RegistrationCompletedEvent>();

            return app;
        }
    }
}
