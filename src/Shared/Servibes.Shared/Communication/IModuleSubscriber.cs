using System;
using System.Threading.Tasks;
using MediatR;

namespace Servibes.Shared.Communication
{
    public interface IModuleSubscriber
    {
        IModuleSubscriber Subscribe<TRequest>(string path, Func<IServiceProvider, TRequest, Task<object>> action)
            where TRequest : class;

        IModuleSubscriber Subscribe<TEvent>() where TEvent : INotification;
    }
}