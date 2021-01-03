using MediatR;
using Microsoft.AspNetCore.Http;
using Servibes.BusinessProfile.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Commands.Images.UploadImage
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, Guid>
    {
        private readonly BusinessProfileContext _context;

        public UploadImageCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public async Task<Guid> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            if (request.ImageData == null)
                throw new ArgumentNullException($"Image data can not be null.");

            if (request.ImageData.Length == 0)
                throw new ArgumentException($"Image data contains no data.");

            if (request.ImageData.ContentType != "image/png" && request.ImageData.ContentType != "image/jpg")
                throw new ArgumentException($"Unupported filetype. Try .png or .jpg file instead.");

            Image image = new Image();
            image.Data = ImageToByteArray(request.ImageData);
            image.FileType = request.ImageData.ContentType;

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return image.ImageId;
        }

        private byte[] ImageToByteArray(IFormFile imageData)
        {
            byte[] result = null;
            using (var readStream = imageData.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                readStream.CopyTo(memoryStream);
                result = memoryStream.ToArray();
            }

            return result;
        }
    }
}
