// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Configurations;
public class MicrologixPlcConfig : IEntityTypeConfiguration<MicrologixPlc>
{
    public void Configure(EntityTypeBuilder<MicrologixPlc> builder)
    {

        //builder.ToTable("MicrologixPlc")
        //    .HasKey(e => e.PlcId)
        //    .HasName("PrimaryKey_PlcId");

        //builder.HasKey(e => e.PlcId);

        //builder.Property(e => e.PlcId)
        //    .ValueGeneratedOnAdd();

        //builder.Property(e => e.Gateway)
        //    .IsRequired()
        //    .HasMaxLength(20);

        //// if no name is provided then set the name to ('PLC' + 'PlcId')
        //builder.Property(e => e.DefaultName)
        //    .HasComputedColumnSql("'PLC-' +  [PlcId]")
        //    .HasMaxLength(50);



        //builder.Property(e => e.PlcType)

        //    .HasMaxLength(50)
        //    .HasConversion<string>();

        //builder.Property(e => e.Protocol)

        //    .HasMaxLength(50)
        //    .HasConversion<string>();

        //builder.Property(e => e.TimeoutSeconds);


    }
}
