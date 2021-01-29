using System;
using System.ComponentModel.DataAnnotations;

namespace Servibes.BusinessProfile.Api.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }

        public string FileType { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Data { get; set; }
    }
}
