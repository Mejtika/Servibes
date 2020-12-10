using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.Appointments.Infrastructure;

namespace Servibes.Appointments.Api
{
    public static class AppointmentsModuleExtensions
    {
        public static IServiceCollection AddAppointmentsModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddInfrastructure(configuration);
        }

        public static IApplicationBuilder UseAppointmentsModule(this IApplicationBuilder app)
        {
            return app
                .UseInfrastructure();
        }
    }
}
