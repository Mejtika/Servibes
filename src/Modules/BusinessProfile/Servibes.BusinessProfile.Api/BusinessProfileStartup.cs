﻿using System;
using System.Collections.Generic;
using System.Text;
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
            return app;
        }
    }
}