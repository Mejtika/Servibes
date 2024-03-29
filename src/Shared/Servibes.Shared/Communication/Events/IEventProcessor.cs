﻿using Servibes.Shared.BuildingBlocks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servibes.Shared.Communication.Events
{
    public interface IEventProcessor
    {
        Task ProcessAsync(IEnumerable<IDomainEvent> events);
    }
}