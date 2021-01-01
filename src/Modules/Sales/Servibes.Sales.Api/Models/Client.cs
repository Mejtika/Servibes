﻿using System;

namespace Servibes.Sales.Api.Models
{
    public class Client
    {
        public Guid ClientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name => $"{FirstName} {LastName}";

        public string Email { get; set; }
    }
}