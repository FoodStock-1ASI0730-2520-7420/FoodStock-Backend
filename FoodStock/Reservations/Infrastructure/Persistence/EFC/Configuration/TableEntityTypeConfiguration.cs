// path: src/Reservations/Infrastructure/Persistence/EFC/Configuration/TableEntityTypeConfiguration.cs
using FoodStock.Reservations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStock.Reservations.Infrastructure.Persistence.EFC.Configuration;

public class TableEntityTypeConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.ToTable("tables");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Number).HasColumnName("number").IsRequired();
        builder.Property(x => x.Capacity).HasColumnName("capacity").IsRequired();

        builder.HasIndex(x => x.Number).IsUnique();
    }
}