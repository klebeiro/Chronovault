using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using chronovault_api.Models;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.OriginalPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Brand)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(p => p.Brand);

        builder.HasIndex(p => p.Category);
    }
}