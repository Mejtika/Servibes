using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.ClientProfile.Api.Events.External.AppointmentPaid;
using Servibes.ClientProfile.Api.Events.External.NewClientRegistered;
using Servibes.Shared;

namespace Servibes.ClientProfile.Api
{
    public static class ClientProfileStartup
    {
        public static IServiceCollection AddClientProfileModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddDbContext<ClientProfileContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsHistoryTable("__ClientProfileMigrationsHistory", "client");
                    });
            });

            return services;
        }

        public static IApplicationBuilder UseClientProfileModule(this IApplicationBuilder app)
        {
            app.UseModuleRequests()
                .Subscribe<AppointmentPaidEvent>()
                .Subscribe<NewClientRegisteredEvent>();

            return app;
        }
    }
}
