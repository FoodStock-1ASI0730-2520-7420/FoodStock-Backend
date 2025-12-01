using FoodStock.Suppliers.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Suppliers.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensionsSuppliers
{
    public static ModelBuilder AddSuppliers(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        return modelBuilder;
    }
}