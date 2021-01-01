using System;
using System.Collections.Generic;

namespace Servibes.Sales.Api.Models
{
    public class WalkInClient
    {
        public Guid WalkInClientId { get; set; }

        public string Name => "Walk-in";
    }
}