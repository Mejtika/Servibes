using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Servibes.BusinessProfile.Api
{
    public class BusinessProfileContext : DbContext
    {
        public BusinessProfileContext(DbContextOptions<BusinessProfileContext> options) : base(options)
        {
        }
    }
}
