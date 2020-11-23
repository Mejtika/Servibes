using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Servibes.Shared.Communication
{
    internal sealed class ModuleSubscriber : IModuleSubscriber
    {
        private readonly IModuleRegistry _registry;

        public ModuleSubscriber(IModuleRegistry registry)
            => _registry = registry;

        public IModuleSubscriber Subscribe<TRequest>(string path, Func<IServiceProvider, TRequest, Task<object>> action) where TRequest : class
        {
            if (!_registry.TryAddRequestAction(path, typeof(TRequest), (sp, o) => action(sp, (TRequest)o)))
            {
                throw new Exception();
            }

            return this;
        }

        public IModuleSubscriber Subscribe<TEvent>() where TEvent : INotification
        {
            _registry.AddBroadcastAction(typeof(TEvent), (sp, @event) =>
            {
                var dispatcher = sp.GetService<IMediator>();
                return dispatcher.Publish(@event);
            });

            return this;
        }
    }
}