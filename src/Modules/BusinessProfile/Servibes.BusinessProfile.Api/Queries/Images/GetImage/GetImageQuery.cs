using MediatR;
using Servibes.BusinessProfile.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Images.GetImage
{
    public class GetImageQuery : IRequest<Image>
    {
        public Guid ImageId { get; }

        public GetImageQuery(Guid imageId)
        {
            ImageId = imageId;
        }
    }
}
