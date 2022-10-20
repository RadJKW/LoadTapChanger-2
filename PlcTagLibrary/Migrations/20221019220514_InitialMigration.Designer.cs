﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlcTagLibrary.Data;

#nullable disable

namespace PlcTagLibrary.Migrations
{
    [DbContext(typeof(LoadTapChangerDBContext))]
    [Migration("20221019220514_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PlcTagLibrary.Models.ListTagsByPlc", b =>
                {
                    b.Property<string>("Gateway")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("RslinxTagName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TagType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Value")
                        .HasColumnType("int");

                    b.ToView("ListTagsByPLC");
                });

            modelBuilder.Entity("PlcTagLibrary.Models.MicrologixPlc", b =>
                {
                    b.Property<int>("PlcId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlcId"), 1L, 1);

                    b.Property<string>("DefaultName")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(64)")
                        .HasComputedColumnSql("('PLC' + '-' + CAST([PlcId] as varchar(10)))", false);

                    b.Property<string>("Gateway")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PlcType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Protocol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("TimeoutSeconds")
                        .HasColumnType("smallint");

                    b.HasKey("PlcId");

                    b.HasIndex(new[] { "PlcId", "Name" }, "IX_MicrologixPlcs_PlcId_Name")
                        .IsUnique()
                        .HasFilter("([Name] IS NOT NULL)");

                    b.ToTable("MicrologixPlcs");
                });

            modelBuilder.Entity("PlcTagLibrary.Models.PlcTag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"), 1L, 1);

                    b.Property<string>("CustomName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("PlcDeviceId")
                        .HasColumnType("int");

                    b.Property<string>("RslinxTagName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("TagType")
                        .HasColumnType("int");

                    b.Property<int?>("Value")
                        .HasColumnType("int");

                    b.HasKey("TagId");

                    b.HasIndex(new[] { "PlcDeviceId" }, "IX_MicrologixTags_PlcDeviceId");

                    b.HasIndex(new[] { "TagId", "CustomName", "RslinxTagName" }, "IX_MicrologixTags_TagId_CustomName_ConfiguredName")
                        .IsUnique()
                        .HasFilter("([CustomName] IS NOT NULL AND [RslinxTagName] IS NOT NULL)");

                    b.ToTable("PlcTags");
                });

            modelBuilder.Entity("PlcTagLibrary.Models.PlcTag", b =>
                {
                    b.HasOne("PlcTagLibrary.Models.MicrologixPlc", "PlcDevice")
                        .WithMany("MicrologixTags")
                        .HasForeignKey("PlcDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_PlcTag_Plc");

                    b.Navigation("PlcDevice");
                });

            modelBuilder.Entity("PlcTagLibrary.Models.MicrologixPlc", b =>
                {
                    b.Navigation("MicrologixTags");
                });
#pragma warning restore 612, 618
        }
    }
}