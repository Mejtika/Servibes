using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Servibes.Availability.Application.Shared;
using Servibes.Shared.Database;

namespace Servibes.Availability.Application.Companies.GetCompanyOpeningHours
{
    public class GetCompanyOpeningHoursQueryHandler : IRequestHandler<GetCompanyOpeningHoursQuery, List<HoursRangeDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnection;

        public GetCompanyOpeningHoursQueryHandler(ISqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<List<HoursRangeDto>> Handle(GetCompanyOpeningHoursQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnection.GetOpenConnection();

            const string sql = "SELECT " +
                               "[DayOfWeek], " +
                               "[IsAvailable], " +
                               "FORMAT([Start], N'hh\\:mm') AS [Start], " +
                               "FORMAT([End], N'hh\\:mm') AS [End] " +
                               "FROM [Servibes].[availability].[OpeningHours]" +
                               "WHERE[CompanyId] = @CompanyId";
            var openingHours = await connection.QueryAsync<HoursRangeDto>(sql, new { request.CompanyId });
            return openingHours.AsList();
        }
    }
}