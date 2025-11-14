using FoodStock.Sales.Domain.Model.Aggregate;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Sales.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySalesConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Sale>().HasKey(sale => sale.Id);
        builder.Entity<Sale>().Property(sale => sale.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Sale>().Property(sale => sale.SaleType).IsRequired().HasConversion<string>();
        builder.Entity<Sale>().Property(sale => sale.PaymentMethod).IsRequired().HasConversion<string>();
        
        builder.Entity<SaleItem>().HasKey(sale => sale.Id);
        builder.Entity<SaleItem>().Property(sale => sale.Id).IsRequired().ValueGeneratedOnAdd();
    }
}