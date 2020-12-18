using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.ClientProfile.Api.Models;

namespace Servibes.ClientProfile.Api
{
    public class ClientProfileContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public ClientProfileContext(DbContextOptions<ClientProfileContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("client");

            modelBuilder.Entity<Review>(builder =>
            {
                builder.Property(x => x.Status).HasConversion(new EnumToStringConverter<ReviewStatus>());
            });

            modelBuilder.Entity<Favorite>(builder =>
            {
                builder.Property<Guid>("FavoriteId");
                builder.HasKey("FavoriteId");
            });
        }
    }
}