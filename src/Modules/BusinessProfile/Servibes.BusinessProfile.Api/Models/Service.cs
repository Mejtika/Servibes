using System;
using System.Collections.Generic;

namespace Servibes.BusinessProfile.Api.Models
{
    public class Service
    {
        public Guid ServiceId { get; set; }

        public string ServiceName { get; set; }

        public decimal Price { get; set; }

        public int Duration { get; set; }

        public string Description { get; set; }

        public Guid CompanyId { get; set; }

        public List<Performer> Performers { get; set; } = new List<Performer>();
    }
}
