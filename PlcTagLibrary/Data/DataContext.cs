// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }

        public virtual DbSet<MicrologixPlc> MicrologixPlcs { get; set; }

        public virtual DbSet<MicrologixTag> MicrologixTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MicrologixPlc>(entity =>
            {
                entity.ToTable("MicrologixPlc");

                entity.Property(e => e.Id).HasColumnName("TagID");

                entity.Property(e => e.Gateway)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlcType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Protocol)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<MicrologixTag>(entity =>
            {
                entity.ToTable("MicrologixTag");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CustomName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlcId).HasColumnName("PlcID");

                entity.Property(e => e.LookupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TagType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasColumnName("Value")
                    // allow null
                    .HasDefaultValue(null);


            });

            // set the primary keys
            modelBuilder.Entity<MicrologixPlc>()
                .HasKey(e => new { e.Name, e.Id });

            modelBuilder.Entity<MicrologixTag>()
                .HasKey(e => new { e.Id, e.PlcId });


            // seed database
            modelBuilder.Entity<MicrologixPlc>().HasData(
                new MicrologixPlc { Id = 1, Name = "Micrologix1100", Gateway = "192.168.0.23", Path = "1,0", Protocol = Protocol.ab_eip, PlcType = PlcType.Slc500, Timeout = TimeSpan.FromMilliseconds(5000) });

            modelBuilder.Entity<MicrologixTag>().HasData(
                new MicrologixTag { Id = 1, CustomName = "Output-1", LookupName = "O0:0/1", TagType = TagType.Output, Value = 0, PlcId = 1 });

            modelBuilder.Entity<MicrologixTag>().HasData(
                new MicrologixTag { Id = 2, CustomName = "Input-1", LookupName = "I1:0/1", TagType = TagType.Input, Value = 0, PlcId = 1 });


        }
    }


}
