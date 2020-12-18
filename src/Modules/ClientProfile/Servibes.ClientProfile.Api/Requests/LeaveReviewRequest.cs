using System;

namespace Servibes.ClientProfile.Api.Requests
{
    public class LeaveReviewRequest
    {
        public Guid ReviewId { get; set; }

        public string Description { get; set; }

        public int StarsCount { get; set; }
    }
}