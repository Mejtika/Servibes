using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.Shared.BuildingBlocks
{
    public class InvalidAggregateIdException : Exception
    {
        public Guid Id { get; }

        public InvalidAggregateIdException(Guid id)
            : base($"Invalid aggregate id: {id}")
            => Id = id;
    }
}
