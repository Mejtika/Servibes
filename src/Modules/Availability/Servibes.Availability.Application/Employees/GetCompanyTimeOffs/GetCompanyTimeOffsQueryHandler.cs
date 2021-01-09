using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Servibes.Shared.Database;
using Servibes.Shared.Services;

namespace Servibes.Availability.Application.Employees.GetCompanyTimeOffs
{
    public class GetCompanyTimeOffsQueryHandler : IRequestHandler<GetCompanyTimeOffsQuery, List<CompanyTimeOffDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnection;
        private readonly IDateTimeServer _dateTimeServer;

        public GetCompanyTimeOffsQueryHandler(
            ISqlConnectionFactory sqlConnection,
            IDateTimeServer dateTimeServer)
        {
            _sqlConnection = sqlConnection;
            _dateTimeServer = dateTimeServer;
        }

        public async Task<List<CompanyTimeOffDto>> Handle(GetCompanyTimeOffsQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnection.GetOpenConnection();
            const string timeOffsSql =
                             "SELECT " +
                             "[Employees].[EmployeeId], " +
                             "[Start], " +
                             "[End] " +
                             "FROM [Servibes].[availability].[TimeOffs] AS [TimeOffs] " +
                             "JOIN [Servibes].[availability].[Employees] AS [Employees] ON [TimeOffs].[EmployeeId] = [Employees].[EmployeeId] " +
                             "JOIN [Servibes].[availability].[Companies] AS [Companies] ON [Employees].CompanyId = [Companies].CompanyId " +
                             "WHERE [Companies].CompanyId = @CompanyId AND @Date >= [TimeOffs].[Start] AND @Date <= [TimeOffs].[End]";

            var timeOffs = (await connection.QueryAsync<CompanyTimeOffDto>(timeOffsSql, new { request.CompanyId, request.Date })).AsList();
            return timeOffs;
        }
    }
}