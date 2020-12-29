using System;
using MediatR;

namespace Servibes.ClientProfile.Api.Events
{
    public class ReviewAddedEvent : INotification
    {
        public Guid ReviewId { get; }

        public Guid ClientId { get; }

        public Guid CompanyId { get; }

        public string Description { get; }

        public int StarsCount { get; }

        public DateTime AddedOn { get; }

        public ReviewAddedEvent(
            Guid reviewId,
            Guid clientId,
            Guid companyId,
            string description,
            int starsCount, 
            DateTime addedOn)
        {
            ReviewId = reviewId;
            ClientId = clientId;
            CompanyId = companyId;
            Description = description;
            StarsCount = starsCount;
            AddedOn = addedOn;
        }
    }
}
