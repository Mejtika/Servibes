using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Models
{
    public class Service
    {
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
    }
}
