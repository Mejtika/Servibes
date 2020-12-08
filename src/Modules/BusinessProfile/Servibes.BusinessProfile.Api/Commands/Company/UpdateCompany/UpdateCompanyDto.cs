namespace Servibes.BusinessProfile.Api.Commands.Company.UpdateCompany
{
    public class UpdateCompanyDto
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string CoverPhoto { get; set; }
        public CompanyAddressDto Address { get; set; }
    }
}
