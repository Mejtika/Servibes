using System;

namespace Servibes.BusinessProfile.Api.ModuleRequests
{
    public class AvailableModuleRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid ServiceId { get; set; }
    }
}