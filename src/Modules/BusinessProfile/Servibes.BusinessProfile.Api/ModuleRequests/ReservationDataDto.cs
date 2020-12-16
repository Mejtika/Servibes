namespace Servibes.BusinessProfile.Api.ModuleRequests
{
    public class ReservationDataDto
    {
        public string EmployeeName { get; set; }

        public string ServiceName { get; set; }

        public decimal ServicePrice { get; set; }

        public int ServiceDuration { get; set; }
    }
}
