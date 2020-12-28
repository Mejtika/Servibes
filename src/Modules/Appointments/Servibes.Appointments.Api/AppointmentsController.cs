using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servibes.Appointments.Application.Appointments.CancelBusinessAppointment;
using Servibes.Appointments.Application.Appointments.CancelBusinessTimeReservation;
using Servibes.Appointments.Application.Appointments.CancelClientAppointment;
using Servibes.Appointments.Application.Appointments.GetAllClientAppointments;

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
        public async Task<IActionResult> CancelClientAppointment(Guid appointmentId, CancelAppointmentRequest request)
        {
            await _mediator.Send(new CancelClientAppointmentCommand(appointmentId, request.CancellationReason));
            return Ok();
        }

        [HttpPost("appointments/{appointmentId}/cancel")]
        public async Task<IActionResult> CancelBusinessAppointment(Guid appointmentId, CancelAppointmentRequest request)
        {
            await _mediator.Send(new CancelBusinessAppointmentCommand(appointmentId, request.CancellationReason));
            return Ok();
        }


        [HttpPost("timeReservations/{timeReservationId}/cancel")]
        public async Task<IActionResult> CancelBusinessTimeReservation(Guid timeReservationId)
        {
            await _mediator.Send(new CancelBusinessTimeReservationCommand(timeReservationId));
            return Ok();
        }
    }
}
