using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Servibes.Availability.Application;

namespace Servibes.Availability.Infrastructure
{
    public class AvailabilityUnitOfWork : IAvailabilityUnitOfWork
    {
        private readonly AvailabilityContext _context;

        public AvailabilityUnitOfWork(AvailabilityContext context)
        {
            _context = context;
        }

        //public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        //{
        //    return await _context.SaveChangesAsync(cancellationToken);
        //}

        public int Commit()
        {
            DisplayStates(_context.ChangeTracker.Entries());
            return _context.SaveChanges();

        }

        private static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                Debug.WriteLine($"*** Entity: {entry.Entity.ToString()} {entry.OriginalValues.ToString()} {entry.Entity.GetType().Name}, State: {entry.State.ToString()} ***");
            }
        }
    }
}
