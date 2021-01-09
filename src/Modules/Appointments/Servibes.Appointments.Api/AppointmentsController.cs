using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servibes.Appointments.Api.Requests;
using Servibes.Appointments.Application.Appointments.CancelBusinessAppointment;
using Servibes.Appointments.Application.Appointments.CancelClientAppointment;
using Servibes.Appointments.Application.Appointments.GetAllClientAppointments;
using Servibes.Appointments.Application.TimeReservations.CancelBusinessTimeReservation;

namespace Servibes.Appointments.Api
{
    [ApiController]
    [Route("api")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("account/appointments")]
        public async Task<IActionResult> GetAllClientAppointments()
        {
            var result = await _mediator.Send(new GetAllClientAppointmentsQuery());
            return Ok(result);
        }

        [HttpPost("account/appointments/{appointmentId}/cancel")]
        public async Task<IActionResult> CancelClientAppointment(Guid appointmentId, [FromBody] CancelAppointmentRequest request)
        {
            await _mediator.Send(new CancelClientAppointmentCommand(appointmentId, request.CancellationReason));
            return Ok();
        }

        [HttpGet("companies/{companyId}/appointments")]
        public async Task<IActionResult> GetAllCompanyAppointments(Guid companyId, DateTime date)
        {
            var result = await _mediator.Send(new GetCompanyAppointmentsQuery(companyId, date));
            return Ok(result);
        }

        [HttpPost("companies/{companyId}/appointments/{appointmentId}/cancel")]
        public async Task<IActionResult> CancelBusinessAppointment(Guid companyId, Guid appointmentId, [FromBody] CancelAppointmentRequest request)
        {
            await _mediator.Send(new CancelBusinessAppointmentCommand(companyId, appointmentId, request.CancellationReason));
            return Ok();
        }

        [HttpGet("companies/{companyId}/timeReservations")]
        public async Task<IActionResult> GetCompanyTimeReservations(Guid companyId, DateTime date)
        {
            var result = await _mediator.Send(new GetCompanyTimeReservationsQuery(companyId, date));
            return Ok(result);
        }

        [HttpPost("companies/{companyId}/timeReservations/{timeReservationId}/cancel")]
        public async Task<IActionResult> CancelBusinessTimeReservation(Guid companyId, Guid timeReservationId)
        {
            await _mediator.Send(new CancelBusinessTimeReservationCommand(companyId, timeReservationId));
            return Ok();
        }
    }
}
