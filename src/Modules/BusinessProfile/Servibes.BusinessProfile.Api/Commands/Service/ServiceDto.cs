using Servibes.BusinessProfile.Api.Commands.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Service
{
    public class ServiceDto
    {
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }
        public List<EmployeeDto> Performers { get; set; }
    }
}
