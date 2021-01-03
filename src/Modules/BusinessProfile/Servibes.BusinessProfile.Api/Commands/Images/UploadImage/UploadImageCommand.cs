using MediatR;
using Microsoft.AspNetCore.Http;
using Servibes.BusinessProfile.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Images.UploadImage
{
    public class UploadImageCommand : IRequest<Guid>
    {
        public IFormFile ImageData { get; }

        public UploadImageCommand(IFormFile imageData)
        {
            ImageData = imageData;
        }
    }
}
