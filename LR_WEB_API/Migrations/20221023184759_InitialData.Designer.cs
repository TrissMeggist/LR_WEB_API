﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LR_WEB_API.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20221023184759_InitialData")]
    partial class InitialData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Entities.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CompanyId");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            Address = "583 Wall Dr. Gwynn Oak, MD 21207",
                            Country = "USA",
                            Name = "IT_Solutions Ltd"
                        },
                        new
                        {
                            Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                            Address = "312 Forest Avenue, BF 923",
                            Country = "USA",
                            Name = "Admin_Solutions Ltd"
                        });
                });

            modelBuilder.Entity("Entities.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EmployeeId");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid?>("PortId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("PortId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                            Age = 26,
                            CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            Name = "Sam Raiden",
                            Position = "Software developer"
                        },
                        new
                        {
                            Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                            Age = 30,
                            CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            Name = "Jana McLeaf",
                            Position = "Software developer"
                        },
                        new
                        {
                            Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                            Age = 35,
                            CompanyId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                            Name = "Kane Miller",
                            Position = "Administrator"
                        });
                });

            modelBuilder.Entity("Entities.Models.Port", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PortsId");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Ports");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991053"),
                            Capacity = 130,
                            Country = "USA",
                            Title = "Port of Baltimore",
                            Type = "Maritime"
                        },
                        new
                        {
                            Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2a70"),
                            Capacity = 75,
                            Country = "Russia",
                            Title = "Northern river port",
                            Type = "River"
                        },
                        new
                        {
                            Id = new Guid("3d490a70-94ce-4d15-9494-5248280c294c"),
                            Capacity = 100,
                            Country = "Russia",
                            Title = "Novorossiysk seaport",
                            Type = "Maritime"
                        });
                });

            modelBuilder.Entity("Entities.Models.Ship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ShipsId");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid>("PortsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("PortsId");

                    b.ToTable("Ships");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80abbca8-664d-4b20-b5de-024705497ca8"),
                            Class = "screw ships class M",
                            PortsId = new Guid("3d490a70-94ce-4d15-9494-5248280c2a70"),
                            Title = "Baikal"
                        },
                        new
                        {
                            Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb8c0"),
                            Class = "cruise ship class + 1A1",
                            PortsId = new Guid("3d490a70-94ce-4d15-9494-5248280c294c"),
                            Title = "Maxim Gorkiy"
                        },
                        new
                        {
                            Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a84793c1"),
                            Class = "Nimitz-class aircraft carrier",
                            PortsId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991053"),
                            Title = "USS Nimitz (CVN-68)"
                        });
                });

            modelBuilder.Entity("Entities.Models.Employee", b =>
                {
                    b.HasOne("Entities.Models.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Port", null)
                        .WithMany("Ships")
                        .HasForeignKey("PortId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Entities.Models.Ship", b =>
                {
                    b.HasOne("Entities.Models.Port", "Port")
                        .WithMany()
                        .HasForeignKey("PortsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Port");
                });

            modelBuilder.Entity("Entities.Models.Company", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Entities.Models.Port", b =>
                {
                    b.Navigation("Ships");
                });
#pragma warning restore 612, 618
        }
    }
}
