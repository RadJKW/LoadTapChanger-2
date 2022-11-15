using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlcTagLib.Entities;
using PlcTagLib.Enums;

namespace PlcTagLib.Data.Configurations;
public class PlcTagConfiguration : IEntityTypeConfiguration<PlcTag>
{
    public void Configure(EntityTypeBuilder<PlcTag> builder)
    {
        builder.HasIndex(t => new { t.SymbolName, t.PlcId }).IsUnique();

        builder.Property(t => t.TagTypeId)
            .HasConversion<int>();
    }
}

public class TagTypeConfiguration : IEntityTypeConfiguration<TagType>
{
    public void Configure(EntityTypeBuilder<TagType> builder)
    {
        builder.HasIndex(t => t.Name).IsUnique();

        builder.Property(t => t.TagTypeId)
            .HasConversion<int>();

        builder.Property(t => t.Name)
            .HasColumnName("TagType")
            .HasMaxLength(20);

        builder.HasData(Enum.GetValues(typeof(TagTypeId))
            .Cast<TagTypeId>()
            .Select(e => new TagType { TagTypeId = e, Name = e.ToString() }));
    }
}
