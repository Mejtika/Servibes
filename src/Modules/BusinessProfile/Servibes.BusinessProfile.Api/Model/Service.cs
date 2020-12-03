using System;
using System.Collections.Generic;

namespace Servibes.BusinessProfile.Api.Model
{
    public class Service
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }
        public List<Performer> Performers { get; set; }
    }
}
