using MediatR;
using System;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetServiceById
{
    public class GetServiceByIdQuery : IRequest<CompanyServicesDto>
    {
        public Guid CompanyId { get; }
        public Guid ServiceId { get; }

        public GetServiceByIdQuery(Guid companyId, Guid serviceId)
        {
            CompanyId = companyId;
            ServiceId = serviceId;
        }
    }
}
