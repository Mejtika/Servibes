using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                .Subscribe<AppointmentFinishedEvent>();

            return app;
        }
    }
}
