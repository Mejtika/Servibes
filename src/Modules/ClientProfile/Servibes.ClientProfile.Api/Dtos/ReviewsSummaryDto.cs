using System.Collections.Generic;

namespace Servibes.ClientProfile.Api.Dtos
{
    public class ReviewsSummaryDto
    {
        public List<ReviewSummaryDto> Reviews { get; set; }

        public double? Average { get; set; }

        public int Count { get; set; }
    }
}