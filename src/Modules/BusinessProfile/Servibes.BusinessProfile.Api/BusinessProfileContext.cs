using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Servibes.BusinessProfile.Api.Model;

namespace Servibes.BusinessProfile.Api
{
    public class BusinessProfileContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public BusinessProfileContext(DbContextOptions<BusinessProfileContext> options) : base(options)
        {
        }
    }
}
