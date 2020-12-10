using System.Collections.Generic;

namespace Servibes.BusinessProfile.Api.Commands.Services
{
    public class ServiceDto
    {
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }
        public List<EmployeeDto> Performers { get; set; }
    }
}
