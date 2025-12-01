using FoodStock.Suppliers.Domain.Model.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStock.Suppliers.Infrastructure.Persistence.EFC.Configuration;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("IdSupplier")
            .ValueGeneratedOnAdd();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(s => s.Contact)
            .HasMaxLength(120);

        builder.Property(s => s.Type)
            .HasMaxLength(60);

        builder.Property(s => s.Email)
            .HasMaxLength(180);

        builder.HasIndex(s => s.Email).IsUnique(false);
    }
}