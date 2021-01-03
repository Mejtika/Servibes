using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
