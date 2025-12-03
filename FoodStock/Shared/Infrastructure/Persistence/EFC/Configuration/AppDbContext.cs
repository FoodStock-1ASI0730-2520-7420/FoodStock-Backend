using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using FoodStock.IAM.Domain.Model.Aggregates;
using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Reservations.Infrastructure.Persistence.EFC.Configuration.Extensions;
using FoodStock.Sales.Infrastructure.Persistence.EFC.Configuration.Extensions;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using FoodStock.Suppliers.Domain.Model.Aggregate;
using FoodStock.Suppliers.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Dish> Dishes { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    /// <summary>
    ///     On configuring the database context
    /// </summary>
    /// <remarks>
    ///     This method is used to configure the database context.
    ///     It also adds the created and updated date interceptor to the database context.
    /// </remarks>
    /// <param name="builder">
    ///     The option builder for the database context
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    /// <summary>
    ///     On creating the database model
    /// </summary>
    /// <remarks>
    ///     This method is used to create the database model for the application.
    /// </remarks>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Sales Context
        builder.ApplySalesConfiguration();
        // Reservations Context
        builder.ApplyReservationsConfiguration();
        builder.AddSuppliers();
        
        //IAM context
        builder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(u => u.Id);

            entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
            entity.Property(u => u.Password).IsRequired();
            entity.Property(u => u.Phone).HasMaxLength(20);
            entity.Property(u => u.Segment).HasMaxLength(50);
            entity.Property(u => u.ProfilePicture).HasMaxLength(250);
            entity.Property(u => u.Plan).HasMaxLength(50);

            entity.HasIndex(u => u.Email).IsUnique();
        });
        
        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}