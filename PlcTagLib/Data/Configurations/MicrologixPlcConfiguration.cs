using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlcTagLib.Entities;

namespace PlcTagLib.Data.Configurations;

public class MicrologixPlcConfiguration : IEntityTypeConfiguration<MicrologixPlc>
{
    public void Configure(EntityTypeBuilder<MicrologixPlc> builder)
    {
        builder.HasMany(t => t.PlcTags)
            .WithOne(t => t.Plc)
            .HasForeignKey(t => t.PlcId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.IpAddress)
            .IsUnique();

        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.IpAddress)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Location)
            .HasMaxLength(200);

        builder.Property(t => t.Program)
            .HasMaxLength(50);
    }
}
