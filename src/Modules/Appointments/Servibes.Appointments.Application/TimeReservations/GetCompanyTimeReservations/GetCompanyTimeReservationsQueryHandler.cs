using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Servibes.Appointments.Core.Reservees;
using Servibes.Appointments.Core.TimeReservations;
using Servibes.Shared.Database;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Api
{
    public class GetCompanyTimeReservationsQueryHandler : IRequestHandler<GetCompanyTimeReservationsQuery, List<TimeReservationDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnection;
        private readonly IHttpContextAccessor _accessor;
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyTimeReservationsQueryHandler(
            ISqlConnectionFactory sqlConnection,
            IHttpContextAccessor accessor,
            ICompanyRepository companyRepository)
        {
            _sqlConnection = sqlConnection;
            _accessor = accessor;
            _companyRepository = companyRepository;
        }

        public async Task<List<TimeReservationDto>> Handle(GetCompanyTimeReservationsQuery request, CancellationToken cancellationToken)
        {
            var ownerId = Guid.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var isAuthorized = await _companyRepository.ExistsByOwnerIdAsync(request.CompanyId, ownerId);
            if (!isAuthorized)
            {
                throw new AppException($"User {ownerId} is not authorized to perform this action.");
            }

            var connection = this._sqlConnection.GetOpenConnection();
            const string timeReservationsSql = "SELECT " +
                                               "[TimeReservationId], " +
                                               "[EmployeeId], " +
                                               "[Start], " +
                                               "[End], " +
                                               "[Status] " +
                                               "FROM [Servibes].[appointments].[TimeReservations] AS [TimeReservations] " +
                                               "WHERE [TimeReservations].[CompanyId] = @CompanyId " +
                                               "AND CAST(@Date AS DATE) = CAST([Start] AS DATE)";

            var timeReservations = (await connection.QueryAsync<TimeReservationDto>(timeReservationsSql, new { request.CompanyId, request.Date })).AsList();
            return timeReservations;
        }
    }
}