﻿using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }

        public static IApplicationBuilder UseBusinessProfileModule(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<BusinessProfileContext>();
            dbContext.SeedData();

            return app;
        }
    }
}
