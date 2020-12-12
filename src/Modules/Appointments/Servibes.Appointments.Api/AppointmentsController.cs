using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servibes.Appointments.Application.Appointments.MakeAppointment;

namespace Servibes.Appointments.Api
{
    [ApiController]
    [Route("api/companies")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{companyId}/employees/{employeeId}/appointments")]
        public async Task<IActionResult> MakeAppointment(Guid companyId, Guid employeeId, [FromBody] MakeAppointmentRequest request)
        {
            await _mediator.Send(new MakeAppointmentCommand(
                companyId, 
                employeeId,
                request.EmployeeName,
                request.ServiceName,
                request.ServicePrive,
                request.ServiceDuration,
                request.Start));

            return NoContent();
        }
    }
}
