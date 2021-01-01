using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Sales.Api;
using Servibes.Sales.Api.Events.External.RegistrationCompleted;
using Servibes.Sales.Api.Models;

namespace Servibes.Availability.Application.Events.External.RegistrationCompleted
{
    public class RegistrationCompletedEventHandler : INotificationHandler<RegistrationCompletedEvent>
    {
        private readonly SalesContext _context;

        public RegistrationCompletedEventHandler(SalesContext context)
        {
            _context = context;
        }

        public async Task Handle(RegistrationCompletedEvent notification, CancellationToken cancellationToken)
        {
            var company = new Company
            {
                CompanyId = notification.CompanyId
            };

            await _context.Companies.AddAsync(company, cancellationToken);

            var walkInClient = new WalkInClient
            {
                WalkInClientId = notification.WalkInClientId,
            };

            await _context.WalkInClients.AddAsync(walkInClient, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}