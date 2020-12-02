﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.BusinessProfile.Api;

namespace Servibes.BusinessProfile.Api.Migrations
{
    [DbContext(typeof(BusinessProfileContext))]
    [Migration("20201202202240_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("business")
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Servibes.BusinessProfile.Api.Model.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Servibes.BusinessProfile.Api.Model.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Servibes.BusinessProfile.Api.Model.Service", b =>
                {
                    b.Property<Guid>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Servibes.BusinessProfile.Api.Model.Company", b =>
                {
                    b.OwnsMany("Servibes.BusinessProfile.Api.Model.OpeningHours", "OpeningHours", b1 =>
                        {
                            b1.Property<int>("OpeningHourId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("DayOfWeek")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<TimeSpan>("From")
                                .HasColumnType("time");

                            b1.Property<bool>("IsOpen")
                                .HasColumnType("bit");

                            b1.Property<TimeSpan>("To")
                                .HasColumnType("time");

                            b1.HasKey("OpeningHourId");

                            b1.HasIndex("CompanyId");

                            b1.ToTable("OpeningHours");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.OwnsOne("Servibes.BusinessProfile.Api.Model.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnName("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FlatNumber")
                                .HasColumnName("FlatNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .HasColumnName("Street")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StreetNumber")
                                .HasColumnName("StreetNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ZipCode")
                                .HasColumnName("ZipCode")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.OwnsOne("Servibes.BusinessProfile.Api.Model.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnName("PhoneNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });
                });

            modelBuilder.Entity("Servibes.BusinessProfile.Api.Model.Employee", b =>
                {
                    b.HasOne("Servibes.BusinessProfile.Api.Model.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Servibes.BusinessProfile.Api.Model.WorkingHours", "WorkingHours", b1 =>
                        {
                            b1.Property<int>("WorkingHourId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("DayOfWeek")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<TimeSpan>("From")
                                .HasColumnType("time");

                            b1.Property<TimeSpan>("To")
                                .HasColumnType("time");

                            b1.HasKey("WorkingHourId");

                            b1.HasIndex("EmployeeId");

                            b1.ToTable("WorkingHours");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });
                });

            modelBuilder.Entity("Servibes.BusinessProfile.Api.Model.Service", b =>
                {
                    b.HasOne("Servibes.BusinessProfile.Api.Model.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Servibes.BusinessProfile.Api.Model.Performer", "Performers", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("PerformerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ServiceId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("ServiceId");

                            b1.ToTable("Performers");

                            b1.WithOwner()
                                .HasForeignKey("ServiceId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
