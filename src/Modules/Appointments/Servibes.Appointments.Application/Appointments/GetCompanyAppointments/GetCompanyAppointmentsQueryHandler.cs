using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Servibes.Appointments.Core.Reservees;
using Servibes.Shared.Database;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Api
{
    public class GetCompanyAppointmentsQueryHandler : IRequestHandler<GetCompanyAppointmentsQuery, List<AppointmentDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnection;
        private readonly IHttpContextAccessor _accessor;
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyAppointmentsQueryHandler(
            ISqlConnectionFactory sqlConnection,
            IHttpContextAccessor accessor,
            ICompanyRepository companyRepository)
        {
            _sqlConnection = sqlConnection;
            _accessor = accessor;
            _companyRepository = companyRepository;
        }

        public async Task<List<AppointmentDto>> Handle(GetCompanyAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var ownerId = Guid.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var isAuthorized = await _companyRepository.ExistsByOwnerIdAsync(request.CompanyId, ownerId);
            if (!isAuthorized)
            {
                throw new AppException($"User {ownerId} is not authorized to perform this action.");
            }

            var connection = this._sqlConnection.GetOpenConnection();
            const string appointmentsSql = "SELECT " +
                                           "[AppointmentId], " +
                                           "[Status], " +
                                           "[EmployeeId], " +
                                           "[EmployeeName], " +
                                           "[Clients].[FirstName] +' ' + [Clients].[LastName] AS [ReserveeName], " +
                                           "[ServiceName], " +
                                           "[ServicePrice], " +
                                           "[Start], " +
                                           "[End] " +
                                           "FROM [Servibes].[appointments].[Appointments] AS [Appointments] " +
                                           "JOIN [Servibes].[appointments].[Clients] AS [Clients] ON [Appointments].ReserveeId = [Clients].ClientId " +
                                           "WHERE [Appointments].[CompanyId] = @CompanyId " +
                                           "AND CAST(@Date AS DATE) = CAST([Start] AS DATE)";

            var appointments = (await connection.QueryAsync<AppointmentDto>(appointmentsSql, new { request.CompanyId, request.Date })).AsList();
            return appointments;
        }
    }
}