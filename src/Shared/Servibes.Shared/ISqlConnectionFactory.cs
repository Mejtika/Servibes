using System.Data;

namespace Servibes.Shared
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
