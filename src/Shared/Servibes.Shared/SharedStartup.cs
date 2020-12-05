using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Servibes.Shared.Communication;
using Servibes.Shared.Communication.Brokers;
using Servibes.Shared.Communication.Events;

namespace Servibes.Shared
{
    public static class SharedStartup
    {
        public static IServiceCollection AddSharedModule(this IServiceCollection services)
        {
            services.AddSingleton<IModuleRegistry, ModuleRegistry>();
            services.AddSingleton<IModuleSubscriber, ModuleSubscriber>();
            services.AddTransient<IModuleClient, ModuleClient>();

            services.AddTransient<IMessageBroker, MessageBroker>();
            services.AddTransient<IEventMapperCompositionRoot, EventMapperCompositionRoot>();
            services.AddTransient<IEventProcessor, EventProcessor>();

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
