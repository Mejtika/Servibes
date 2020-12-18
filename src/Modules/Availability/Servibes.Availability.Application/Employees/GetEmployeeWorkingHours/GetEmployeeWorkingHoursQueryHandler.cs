using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Servibes.Availability.Application.Shared;
using Servibes.Shared.Database;
using Servibes.Shared.Exceptions;

namespace Servibes.Availability.Application.Employees.GetEmployeeWorkingHours
{
    public class GetEmployeeWorkingHoursQueryHandler : IRequestHandler<GetEmployeeWorkingHoursQuery, List<HoursRangeDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnection;

        public GetEmployeeWorkingHoursQueryHandler(ISqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<List<HoursRangeDto>> Handle(GetEmployeeWorkingHoursQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnection.GetOpenConnection();

            const string employeeAvailabilitySql = "SELECT" +
                                                   "[EmployeeId], " +
                                                   "[CompanyId] " +
                                                   "FROM [Servibes].[availability].[Employees]" +
                                                   "WHERE [EmployeeId] = @EmployeeId AND [CompanyId] = @CompanyId";
            var employeeAvailability  = await connection.QuerySingleOrDefaultAsync<EmployeeAvailabilityDto>(employeeAvailabilitySql, new { request.EmployeeId, request.CompanyId });

            if (employeeAvailability == null)
            {
                throw new AppException("Employee or company with specified id doesn't exists.");
            }


            const string sql = "SELECT " +
                               "[DayOfWeek], " +
                               "[IsAvailable], " +
                               "FORMAT([Start], N'hh\\:mm') AS [Start], " +
                               "FORMAT([End], N'hh\\:mm') AS [End] " +
                               "FROM [Servibes].[availability].[WorkingHours]" +
                               "WHERE[EmployeeId] = @EmployeeId";
            var workingHours = await connection.QueryAsync<HoursRangeDto>(sql, new { request.EmployeeId });
            return workingHours.AsList();
        }
    }
}
