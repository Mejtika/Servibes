using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servibes.Sales.Api.Models;

namespace Servibes.Sales.Api
{
    [ApiController] 
    [Route("api/sales/appointments")]
    public class SalesController : ControllerBase
    {
        private readonly SalesContext _context;
        private readonly AuthorizationClient _authorizationClient;

        public SalesController(
            SalesContext _context,
            AuthorizationClient authorizationClient)
        {
            this._context = _context;
            _authorizationClient = authorizationClient;
        }

        [HttpPost("{appointmentId}/checkout")]
        public async Task<IActionResult> Checkout(Guid appointmentId)
        {
            var appointment = await _context.Appointments.SingleOrDefaultAsync(x => x.AppointmentId == appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _authorizationClient.IsAuthenticatedAsync(userId, appointment.CompanyId);

            if (!user.IsAuthorized)
            {
                return Unauthorized();
            }

            appointment.Status = AppointmentStatus.Paid;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
