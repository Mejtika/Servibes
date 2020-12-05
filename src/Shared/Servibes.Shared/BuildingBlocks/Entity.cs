using System;
using System.Collections.Generic;
using System.Linq;
using Servibes.Shared.Communication;

namespace Servibes.Shared.BuildingBlocks
{
    public abstract class Entity
    {
        private List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();
            this._domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
