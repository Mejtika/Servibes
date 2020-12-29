using System.Collections.Generic;
using MediatR;

namespace Servibes.Appointments.Application.Appointments.GetAllClientAppointments
{
    public class GetAllClientAppointmentsQuery : IRequest<List<ClientAppointmentDto>>
    {
    }
}
