using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using chronovault_api.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        var splitStringConverter = new ValueConverter<List<string>, string>(
        v => string.Join(";", v ?? new List<string>()),
        v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
        );

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Brand)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(p => p.Brand);

        builder.HasIndex(p => p.Category);

        builder.Property(p => p.Images)
            .HasConversion(splitStringConverter);
    }
}