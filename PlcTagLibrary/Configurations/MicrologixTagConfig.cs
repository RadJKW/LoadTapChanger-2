// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Formats.Asn1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlcTagLibrary.Models;

namespace PlcTagLibrary.Configurations;
public class MicrologixTagConfig : IEntityTypeConfiguration<PlcTag>
{
    public void Configure(EntityTypeBuilder<PlcTag> builder)

    {
        // not implemented 
        throw new NotImplementedException();
    }
    //#region Entity Configuration...
    //builder
    //    .ToTable("MicrologixTag");
    //builder
    //    .HasKey(e => e.TagId)
    //    .HasName("PrimaryKey_TagId");

    //builder
    //    .HasIndex(e => new { e.TagId, e.CustomName })
    //    .IsUnique();
    //#endregion

    //#region Properties Configuration...
    //builder.Property(e => e.TagId)
    //    .ValueGeneratedOnAdd();

    //builder.Property(e => e.CustomName)
    //    .IsRequired()
    //    .HasMaxLength(50);

    //builder.Property(e => e.PlcDeviceId);

    //builder.Property(e => e.ConfiguredName)
    //    .IsRequired()
    //    .HasMaxLength(50);

    //builder.Property(e => e.TagType)
    //    .IsRequired()
    //    .HasMaxLength(50)
    //    .HasConversion<string>();

    //builder.Property(e => e.Value)
    //    .HasDefaultValue(null);

    //builder.HasOne(e => e.PlcDevice)
    //    .WithMany(e => e.PlcTags)
    //    .HasForeignKey(e => e.PlcDeviceId);
    //#endregion
}

