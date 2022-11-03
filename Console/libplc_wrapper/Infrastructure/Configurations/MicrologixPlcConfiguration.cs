using ConsoleTestsPLC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ConsoleTestsPLC.Infrastructure.Configurations;
public class MicrologixPlcConfiguration : IEntityTypeConfiguration<MicrologixPlc>
{
    public void Configure(EntityTypeBuilder<MicrologixPlc> builder)
    {
        builder.Property(plc => plc.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(plc => plc.IpAddress)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(plc => plc.Location)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(plc => plc.Description)
            .HasMaxLength(200);
    }
}

