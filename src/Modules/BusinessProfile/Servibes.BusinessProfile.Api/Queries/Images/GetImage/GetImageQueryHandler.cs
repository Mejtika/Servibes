using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.BusinessProfile.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Queries.Images.GetImage
{
    public class GetImageQueryHandler : IRequestHandler<GetImageQuery, Image>
    {
        private readonly BusinessProfileContext _context;

        public GetImageQueryHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public async Task<Image> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            var image = await _context.Images.Where(i => i.ImageId == request.ImageId).FirstOrDefaultAsync();

            if (image == null)
                throw new ArgumentNullException($"Image with id {request.ImageId} doesnt exist.");

            return image;
        }
    }
}
