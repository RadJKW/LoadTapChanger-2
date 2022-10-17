// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Data;
public class DataContext : DbContext
{
    public DataContext()
    {

    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {


    }

    public virtual DbSet<MicrologixPlc> MicrologixPlcs { get; set; }

    public virtual DbSet<MicrologixTag> MicrologixTags { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{

    //        )
    //    //{
    //    //    entity.ToTable("MicrologixPlc");

    //    //    entity.Property(e => e.Id).HasColumnName("ID");

    //    //    entity.Property(e => e.Gateway)
    //    //        .IsRequired()
    //    //        .HasMaxLength(15)
    //    //        .IsUnicode(false);

    //    //    entity.Property(e => e.Name)
    //    //        .IsRequired()
    //    //        .HasMaxLength(50)
    //    //        .IsUnicode(false);

    //    //    entity.Property(e => e.PlcType)
    //    //        .IsRequired()
    //    //        .HasMaxLength(50)
    //    //        .IsUnicode(false);


    //    //});


    //}
}
