using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servibes.Sales.Api.Events;
using Servibes.Sales.Api.Models;
using Servibes.Shared.Communication.Brokers;
using Servibes.Shared.Exceptions;

namespace Servibes.Sales.Api
{
    [ApiController] 
    [Route("api/sales/appointments")]
    public class SalesController : ControllerBase
    {
        private readonly SalesContext _context;
        private readonly AuthorizationClient _authorizationClient;
        private readonly IMessageBroker _messageBroker;

        public SalesController(
            SalesContext _context,
            AuthorizationClient authorizationClient,
            IMessageBroker messageBroker)
        {
            this._context = _context;
            _authorizationClient = authorizationClient;
            _messageBroker = messageBroker;
        }

        [HttpPost("{appointmentId}/checkout")]
        public async Task<IActionResult> Checkout(Guid appointmentId)
        {
            var appointment = await _context.Appointments.SingleOrDefaultAsync(x => x.AppointmentId == appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }

            var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var user = await _authorizationClient.IsAuthenticatedAsync(userId, appointment.CompanyId);

            if (!user.IsAuthorized)
            {
                throw new AppException($"User {userId} is not authorized to perform this action.");
            }

            appointment.Status = AppointmentStatus.Paid;

            await _context.SaveChangesAsync();
            var @event = new AppointmentPaidEvent(
                appointment.AppointmentId,
                appointment.ReserveeId,
                appointment.CompanyId,
                appointment.EmployeeId,
                appointment.Price);
            await _messageBroker.PublishAsync(new[] { @event });

            return Ok();
        }
    }
}
