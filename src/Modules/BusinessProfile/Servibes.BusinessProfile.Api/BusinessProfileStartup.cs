﻿using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.BusinessProfile.Api.Events.External.AppointmentCreated;
using Servibes.BusinessProfile.Api.Events.External.ClientInformationUpdated;
using Servibes.BusinessProfile.Api.Events.External.EmployeeDeleted;
using Servibes.BusinessProfile.Api.Events.External.NewClientRegistered;
using Servibes.BusinessProfile.Api.Events.External.ReviewAdded;
using Servibes.BusinessProfile.Api.Services;
using Servibes.BusinessProfile.Api.Services.Reservation;
using Servibes.Shared;

namespace Servibes.BusinessProfile.Api
{
    public static class BusinessProfileStartup
    {
        public static IServiceCollection AddBusinessProfileModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddDbContext<BusinessProfileContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsHistoryTable("__BusinessProfileMigrationsHistory", "business");
                    });
            });

            services.AddTransient<ReservationService>();
            return services;
        }

        public static IApplicationBuilder UseBusinessProfileModule(this IApplicationBuilder app)
        {
            app.UseModuleRequests()
                .Subscribe<AppointmentCreatedEvent>()
                .Subscribe<ReviewAddedEvent>()
                .Subscribe<NewClientRegisteredEvent>()
                .Subscribe<ClientInformationUpdatedEvent>()
                .Subscribe<EmployeeDeletedEvent>()
                .Subscribe<GetReservationDataRequest>("modules/business/details", async (sp, request) =>
                {
                    var service = sp.GetService<ReservationService>();
                    return await service.GetReservationData(request.EmployeeId, request.ServiceId);
                });
                
            using var serviceScope = app.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<BusinessProfileContext>();
            dbContext.SeedData();

            return app;
        }
    }
}
