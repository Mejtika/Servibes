using Servibes.Bootstrapper.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Servibes.Bootstrapper.Data
{
    public class IdentityContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public IdentityContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("identity");
        }
    }
}
