using System.Data;

namespace Servibes.Shared.Database
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
