﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlcTagLib.Data;

#nullable disable

namespace PlcTagLib.Data.Migrations
{
    [DbContext(typeof(PlcTagLibDbContext))]
    partial class PlcTagLibDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PlcTagLib.Entities.MicrologixPlc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DebugLevel")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("PlcType")
                        .HasColumnType("int");

                    b.Property<string>("Program")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Protocol")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Timeout")
                        .HasColumnType("time(0)");

                    b.HasKey("Id");

                    b.HasIndex("IpAddress")
                        .IsUnique();

                    b.ToTable("MicrologixPlcs");
                });

            modelBuilder.Entity("PlcTagLib.Entities.PlcTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlcId")
                        .HasColumnType("int");

                    b.Property<string>("SymbolName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TagTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("BitValue")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PlcId");

                    b.HasIndex("TagTypeId");

                    b.HasIndex("SymbolName", "PlcId")
                        .IsUnique()
                        .HasFilter("[SymbolName] IS NOT NULL");

                    b.ToTable("PlcTags");
                });

            modelBuilder.Entity("PlcTagLib.Entities.TagType", b =>
                {
                    b.Property<int>("TagTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("TagType");

                    b.HasKey("TagTypeId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[TagType] IS NOT NULL");

                    b.ToTable("TagTypes");

                    b.HasData(
                        new
                        {
                            TagTypeId = 0,
                            Name = "Output"
                        },
                        new
                        {
                            TagTypeId = 1,
                            Name = "Input"
                        },
                        new
                        {
                            TagTypeId = 2,
                            Name = "Status"
                        },
                        new
                        {
                            TagTypeId = 3,
                            Name = "Binary"
                        },
                        new
                        {
                            TagTypeId = 4,
                            Name = "Timer"
                        },
                        new
                        {
                            TagTypeId = 5,
                            Name = "Counter"
                        },
                        new
                        {
                            TagTypeId = 6,
                            Name = "Control"
                        },
                        new
                        {
                            TagTypeId = 7,
                            Name = "Integer"
                        },
                        new
                        {
                            TagTypeId = 8,
                            Name = "Float"
                        },
                        new
                        {
                            TagTypeId = 99,
                            Name = "Unknown"
                        });
                });

            modelBuilder.Entity("PlcTagLib.Entities.PlcTag", b =>
                {
                    b.HasOne("PlcTagLib.Entities.MicrologixPlc", "Plc")
                        .WithMany("PlcTags")
                        .HasForeignKey("PlcId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlcTagLib.Entities.TagType", "TagType")
                        .WithMany()
                        .HasForeignKey("TagTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plc");

                    b.Navigation("TagType");
                });

            modelBuilder.Entity("PlcTagLib.Entities.MicrologixPlc", b =>
                {
                    b.Navigation("PlcTags");
                });
#pragma warning restore 612, 618
        }
    }
}
