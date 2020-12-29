    using System.Threading;
using System.Threading.Tasks;
using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Servibes.ClientProfile.Api.Models;

namespace Servibes.ClientProfile.Api.Events.External.AppointmentPaid
{
    public class AppointmentPaidEventHandler : INotificationHandler<AppointmentPaidEvent>
    {
        private readonly ClientProfileContext _context;

        public AppointmentPaidEventHandler(ClientProfileContext context)
        {
            _context = context;
        }

        public async Task Handle(AppointmentPaidEvent notification, CancellationToken cancellationToken)
        {
            var reviewExists = await _context.Reviews.SingleOrDefaultAsync(x =>
                x.CompanyId == notification.CompanyId && x.ClientId == notification.ReserveeId, cancellationToken) != null;
            if (reviewExists)
            {
                return;
            }

            var review = new Review
            {
                CompanyId = notification.CompanyId,
                ClientId = notification.ReserveeId,
                Description = null,
                StarsCount = null,
                Status = ReviewStatus.New
            };

            await _context.Reviews.AddAsync(review, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}