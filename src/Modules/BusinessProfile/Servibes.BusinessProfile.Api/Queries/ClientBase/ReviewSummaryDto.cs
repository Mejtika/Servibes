namespace Servibes.BusinessProfile.Api.Queries.ClientBase
{
    public class ReviewSummaryDto
    {
        public int? Rating { get; set; }

        public int Count { get; set; }

        public int PercentOfTotal { get; set; }
    }
}