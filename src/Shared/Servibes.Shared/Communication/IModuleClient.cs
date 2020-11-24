using System.Threading.Tasks;

namespace Servibes.Shared.Communication
{
    public interface IModuleClient
    {
        Task<TResult> GetAsync<TResult>(string path, object moduleRequest) where TResult : class;
        Task PublishAsync(object moduleBroadcast);
    }
}
