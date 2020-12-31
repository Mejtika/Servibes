using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.ClientBase.GetAllClients
{
    public class GetAllClientsQuery : IRequest<IEnumerable<ClientDto>>
    {
        public Guid CompanyId { get; set; }

        public GetAllClientsQuery(Guid companyId)
        {
            this.CompanyId = companyId;
        }
    }
}
