using ConsoleTestsPLC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleTestsPLC.Infrastructure.Configurations;
public class PlcTagConfiguration : IEntityTypeConfiguration<IntPlcTag>
{
    public void Configure(EntityTypeBuilder<IntPlcTag> builder)
    {
        builder.Property(tag => tag.SymbolName)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(tag => tag.Address)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(tag => tag.Description)
            .HasMaxLength(200);
    }
}
