﻿using System;

namespace Servibes.ClientProfile.Api.Models
{
    public class Review
    {
        public Guid ReviewId { get; set; }

        public Guid ClientId { get; set; }

        public Guid CompanyId { get; set; }

        public string Description { get; set; }

        public int? StarsCount { get; set; }

        public DateTime? AddedOn { get; set; }

        public ReviewStatus Status { get; set; }
    }
}