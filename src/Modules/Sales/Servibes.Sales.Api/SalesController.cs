using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servibes.Sales.Api.Dtos;
using Servibes.Sales.Api.Events;
using Servibes.Sales.Api.Models;
using Servibes.Sales.Api.Requests;
using Servibes.Shared.Communication.Brokers;
using Servibes.Shared.Exceptions;

namespace Servibes.Sales.Api
{
    [ApiController]
    [Route("api")]
    public class SalesController : ControllerBase
    {
        private readonly SalesContext _context;
        private readonly IMessageBroker _messageBroker;

        public SalesController(
            SalesContext context,
            IMessageBroker messageBroker)
        {
            _context = context;
            _messageBroker = messageBroker;
        }

        [HttpGet("account/sales/appointments")]
        public async Task<IActionResult> GetAccountSalesInfo()
        {
            var ownerId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var paidAppointments = await _context.Appointments
                .Where(x => x.Status == AppointmentStatus.Paid && x.ReserveeId == ownerId)
                .ToListAsync();
            var paidAppointmentsDto = paidAppointments.Select(x =>
                new AccountAppointmentDto
                {
                    AppointmentId = x.AppointmentId,
                    Price = x.Price
                }).ToList();

            return Ok(paidAppointmentsDto);
        }

        [HttpGet("sales/appointments")]
        public async Task<IActionResult> GetCompanySalesInfo()
        {
            var ownerId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var ownerCompany = await _context.Companies.SingleOrDefaultAsync(x => x.OwnerId == ownerId);
            if (ownerCompany == null)
            {
                throw new AppException($"User {ownerId} is not authorized to perform this action.");
            }

            var sql = @"SELECT 
                       [Appointments].[AppointmentId],
                       [Clients].FirstName + ' ' + [Clients].LastName AS ClientName,
                       [Employees].[Name] AS EmployeeName,
                       [Appointments].[Price],
                       [Appointments].[ServiceName],
                       [Appointments].[Start],
                       [Appointments].[End]
                       FROM [Servibes].[sales].[Appointments] AS [Appointments]
                       JOIN [Servibes].[sales].[Clients] AS[Clients] ON [Appointments].[ReserveeId] = [Clients].[ClientId]
                       JOIN [Servibes].[sales].Employees AS[Employees] ON [Appointments].[EmployeeId] = [Employees].[EmployeeId]
                       WHERE [Appointments].CompanyId = @companyId AND [Appointments].Status = 'Unpaid'";

            var appointmentsToBePaid = (await _context.Database.GetDbConnection()
                .QueryAsync<AppointmentDto>(sql, new { ownerCompany.CompanyId })).AsList();

            return Ok(appointmentsToBePaid);
        }

        [HttpGet("sales/appointments/history")]
        public async Task<IActionResult> GetCompanySalesHistory()
        {
            var ownerId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var ownerCompany = await _context.Companies.SingleOrDefaultAsync(x => x.OwnerId == ownerId);
            if (ownerCompany == null)
            {
                throw new AppException($"User {ownerId} is not authorized to perform this action.");
            }

            var sql = @"SELECT 
                       [Appointments].[AppointmentId],
                       [Clients].FirstName + ' ' + [Clients].LastName AS ClientName,
                       [Employees].[Name] AS EmployeeName,
                       [Appointments].[Price],
                       [Appointments].[ServiceName],
                       [Appointments].[Start],
                       [Appointments].[End]
                       FROM [Servibes].[sales].[Appointments] AS [Appointments]
                       JOIN [Servibes].[sales].[Clients] AS[Clients] ON [Appointments].[ReserveeId] = [Clients].[ClientId]
                       JOIN [Servibes].[sales].Employees AS[Employees] ON [Appointments].[EmployeeId] = [Employees].[EmployeeId]
                       WHERE [Appointments].CompanyId = @companyId AND [Appointments].Status = 'Paid'";

            var paidAppointments = (await _context.Database.GetDbConnection()
                .QueryAsync<AppointmentDto>(sql, new { ownerCompany.CompanyId })).AsList();

            return Ok(paidAppointments);
        }

        [HttpPost("sales/appointments/{appointmentId}/checkout")]
        public async Task<IActionResult> Checkout(Guid appointmentId, [FromBody] CheckoutRequest request)
        {
            var appointment = await _context.Appointments.SingleOrDefaultAsync(x => x.AppointmentId == appointmentId);
            if (appointment == null)
            {
                throw new AppException($"Appointment with id {appointmentId} not found.");
            }

            var ownerId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var ownerCompany = await _context.Companies.SingleOrDefaultAsync(x => x.OwnerId == ownerId);
            if (ownerCompany == null)
            {
                throw new AppException($"User {ownerId} is not authorized to perform this action.");
            }

            appointment.Status = AppointmentStatus.Paid;
            appointment.Price = request.Price;

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
