using FoodStock.Inventory.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStock.Inventory.Infrastructure.Persistence.EFC.Configuration;
public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.UnitPrice).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Quantity).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Category).HasMaxLength(100);
        builder.Property(p => p.Deleted).HasDefaultValue(false);
        builder.Ignore(p => p.TotalPrice); // calculada
        builder.HasIndex(p => p.Name);
    }
}