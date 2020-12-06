using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Services
{
    public class CompanyServicesDto
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }
    }
}
