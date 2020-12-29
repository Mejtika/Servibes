using System.Collections.Generic;

namespace Servibes.BusinessProfile.Api.Queries.ClientBase
{
    public class ReviewsSummaryDto
    {
        public List<ReviewSummaryDto> Reviews { get; set; }

        public double? Average { get; set; }

        public int Count { get; set; }
    }
}