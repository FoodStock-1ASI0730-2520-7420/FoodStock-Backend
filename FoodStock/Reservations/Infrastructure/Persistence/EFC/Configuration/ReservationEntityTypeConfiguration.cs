// path: src/Reservations/Infrastructure/Persistence/EFC/Configuration/ReservationEntityTypeConfiguration.cs
using FoodStock.Reservations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStock.Reservations.Infrastructure.Persistence.EFC.Configuration;

public class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservations");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");

        builder.Property(x => x.TableNumber).HasColumnName("table_number").IsRequired();
        builder.Property(x => x.QuantityPeople).HasColumnName("quantity_people").IsRequired();
        builder.Property(x => x.ReservationDate).HasColumnName("reservation_date").HasMaxLength(10).IsRequired();
        builder.Property(x => x.ReservationTime).HasColumnName("reservation_time").HasMaxLength(5).IsRequired();
        builder.Property(x => x.CreationDate).HasColumnName("creation_date").HasMaxLength(10);
        builder.Property(x => x.CreationTime).HasColumnName("creation_time").HasMaxLength(5);
        builder.Property(x => x.DurationMinutes).HasColumnName("duration_minutes").IsRequired();
        builder.Property(x => x.Status).HasColumnName("status").HasMaxLength(20).IsRequired();
        builder.Property(x => x.CustomerName).HasColumnName("customer_name").HasMaxLength(120);
        builder.Property(x => x.CustomerPhone).HasColumnName("customer_phone").HasMaxLength(40);

        // índice útil para búsquedas del día/mesa
        builder.HasIndex(x => new { x.ReservationDate, x.TableNumber });
    }
}