using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Servibes.Shared.Communication
{
    internal sealed class ModuleClient : IModuleClient
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IModuleRegistry _moduleRegistry;

        public ModuleClient(IServiceProvider serviceProvider, IModuleRegistry moduleRegistry)
        {
            _serviceProvider = serviceProvider;
            _moduleRegistry = moduleRegistry;
        }

        public async Task<TResult> GetAsync<TResult>(string path, object moduleRequest) where TResult : class
        {
            // Pobieramy nasza rejestracje, dane po kluczu (endpoint)
            var registration = _moduleRegistry.GetRequestRegistration(path);
            if (registration is null)
            {
                throw new InvalidOperationException($"No action has been defined for path: {path}");
            }

            var moduleRequestType = moduleRequest.GetType();

            // Wyciągamy sobie konkretną akcję -> Func zwracający object.
            var action = registration.Action;

            // Następnie dokonujemy translacji typu.

            var receiverRequest = TranslateType(
                moduleRequest, // Nasz anonim z movieId
                registration.ReceiverType); // MovieModuleRequest

            // Następnie wywołujemy tą akcję i w rezulacie otrzymujemy object.
            // Czyli tutaj skoczymy do metody zadeklarowanej w Subscribe.
            var result = await action(_serviceProvider, receiverRequest);

            // Następnie mielibyśmy translację na JSONa
            var resultJson = JsonConvert.SerializeObject(result);

            return JsonConvert.DeserializeObject<TResult>(resultJson);
        }

        public async Task PublishAsync(object moduleBroadcast)
        {
            var tasks = new List<Task>();
            var path = moduleBroadcast.GetType().Name;

            //Szukamy rejestracji po typie wiadomości.
            var registrations = _moduleRegistry
                .GetBroadcastRegistration(path)
                .Where(r => r.ReceiverType != moduleBroadcast.GetType());

            // Tutaj możemy mieć 0 .. N receiverów. 
            foreach (var registration in registrations)
            {
                var action = registration.Action;
                var receiverBroadcast = TranslateType(moduleBroadcast, registration.ReceiverType);
                tasks.Add(action(_serviceProvider, receiverBroadcast));
            }
            await Task.WhenAll(tasks);
        }

        private static object TranslateType(object @object, Type type)
        {
            var json = JsonConvert.SerializeObject(@object);
            var receiverType = JsonConvert.DeserializeObject(json, type);
            return receiverType;
        }
    }
}