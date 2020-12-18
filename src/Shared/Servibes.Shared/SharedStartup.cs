using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servibes.Shared.Communication;
using Servibes.Shared.Communication.Brokers;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Database;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.Shared
{
    public static class SharedStartup
    {
        public static IServiceCollection AddSharedModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IModuleRegistry, ModuleRegistry>();
            services.AddSingleton<IModuleSubscriber, ModuleSubscriber>();
            services.AddTransient<IModuleClient, ModuleClient>();
            services.AddTransient<IMessageBroker, MessageBroker>();
            services.AddTransient<IEventMapperCompositionRoot, EventMapperCompositionRoot>();
            services.AddTransient<IEventProcessor, EventProcessor>();
            services.AddTransient<IDateTimeServer, DateTimeServer>();
            services.AddScoped<ISqlConnectionFactory>(x =>
                    new SqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

            services.AddProblemDetails(x =>
            {
                x.Map<DomainException>(ex => new DomainExceptionProblemDetails(ex));
                x.Map<AppException>(ex => new ApplicationExceptionProblemDetails(ex));
                x.IncludeExceptionDetails = (context, exception) => false;
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));

            return services;
        }

        public static IApplicationBuilder UseSharedModule(this IApplicationBuilder app)
        {
            return app;
        }

        public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
            => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();

    }
}
