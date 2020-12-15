using Servibes.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.BusinessProfile.Api.Queries.Employees;
using Servibes.Shared.Database;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetServiceEmployees
{
    public class GetServiceEmployeesQueryHandler : IRequestHandler<GetServiceEmployeesQuery, IEnumerable<CompanyEmployeeDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnection;

        public GetServiceEmployeesQueryHandler(ISqlConnectionFactory sqlConnection)
        {
            this._sqlConnection = sqlConnection;
        }

        public async Task<IEnumerable<CompanyEmployeeDto>> Handle(GetServiceEmployeesQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnection.GetOpenConnection();

            const string employeesForServiceSql = @"    SELECT 
                                                            E.EmployeeId,
	                                                        E.FirstName,
	                                                        E.LastName
                                                        FROM
                                                            [business].[Performers] P
                                                            INNER JOIN[business].[Employees] E ON E.EmployeeId = P.PerformerId
                                                        WHERE
                                                            P.ServiceId = @ServiceId";
            var employeesForService = await connection.QueryAsync<CompanyEmployeeDto>(employeesForServiceSql, new { request.ServiceId });

            if (employeesForService == null)
            {
                throw new InvalidOperationException("Service doesn't exists");
            }

            return employeesForService.AsList();
        }
    }
}
