﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.Sales.Api;

namespace Servibes.Sales.Api.Migrations
{
    [DbContext(typeof(SalesContext))]
    partial class SalesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("sales")
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Servibes.Sales.Api.Models.Appointment", b =>
                {
                    b.Property<Guid>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ReserveeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppointmentId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ReserveeId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Servibes.Sales.Api.Models.Client", b =>
                {
                    b.Property<Guid>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Servibes.Sales.Api.Models.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WalkInClientId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Servibes.Sales.Api.Models.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Servibes.Sales.Api.Models.Appointment", b =>
                {
                    b.HasOne("Servibes.Sales.Api.Models.Company", null)
                        .WithMany("Appointments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Servibes.Sales.Api.Models.Employee", null)
                        .WithMany("Appointments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Servibes.Sales.Api.Models.Client", null)
                        .WithMany("Appointments")
                        .HasForeignKey("ReserveeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
