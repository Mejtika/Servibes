using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.BusinessProfile.Api.Models.ClientBase;

namespace Servibes.BusinessProfile.Api.Events.External.ReviewAdded
{
    public class ReviewAddedEventHandler : INotificationHandler<ReviewAddedEvent>
    {
        private readonly BusinessProfileContext _context;

        public ReviewAddedEventHandler(BusinessProfileContext context)
        {
            _context = context;
        }

        public async Task Handle(ReviewAddedEvent notification, CancellationToken cancellationToken)
        {
            var review = new Review
            {
                ReviewId = notification.ReviewId,
                ClientId = notification.ClientId,
                CompanyId = notification.CompanyId,
                Description = notification.Description,
                StarsCount = notification.StarsCount,
                AddedOn = notification.AddedOn
            };

            await _context.Reviews.AddAsync(review, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}