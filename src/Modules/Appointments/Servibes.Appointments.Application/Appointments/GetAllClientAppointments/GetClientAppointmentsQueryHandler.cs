using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Servibes.Shared.Database;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Application.Appointments.GetAllClientAppointments
{
    public class GetClientAppointmentsQueryHandler : IRequestHandler<GetAllClientAppointmentsQuery, List<ClientAppointmentDto>>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ISqlConnectionFactory _sqlConnection;

        public GetClientAppointmentsQueryHandler(
            IHttpContextAccessor accessor,
            ISqlConnectionFactory sqlConnection)
        {
            _accessor = accessor;
            _sqlConnection = sqlConnection;
        }

        public async Task<List<ClientAppointmentDto>> Handle(GetAllClientAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var ownerId = Guid.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var connection = this._sqlConnection.GetOpenConnection();
            const string sql = "SELECT " +
                               "[AppointmentId], " +
                               "[CompanyId], " +
                               "[Status], " +
                               "[EmployeeId], " +
                               "[EmployeeName], " +
                               "[ServiceName], " +
                               "[ServicePrice], " +
                               "[Start], " +
                               "[End], " +
                               "[CancellationReason] " +
                               "FROM [Servibes].[appointments].[Appointments]" +
                               "WHERE [ReserveeId] = @ownerId " +
                               "ORDER BY [Start] DESC";

            var appointments = (await connection.QueryAsync<ClientAppointmentDto>(sql, new { ownerId })).AsList();

            if (appointments == null)
            {
                throw new AppException($"Appointments for client {ownerId} not found.");
            }

            return appointments;
        }
    }
}