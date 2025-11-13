using FoodStock.Inventory.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStock.Inventory.Infrastructure.Persistence.EFC.Configuration;

public class DishEntityTypeConfiguration : IEntityTypeConfiguration<Dish>
{
    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.ToTable("dishes");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.PriceUnit) // ✅ nuevo campo
    .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.OwnsMany(d => d.Ingredients, ib =>
        {
            ib.ToTable("dish_ingredients");
            ib.WithOwner().HasForeignKey(di => di.DishId);
            ib.HasKey(di => di.Id);

            ib.Property(di => di.ProductId).IsRequired();
            ib.Property(di => di.Quantity)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            ib.HasIndex(di => di.ProductId);
        });
    }
}