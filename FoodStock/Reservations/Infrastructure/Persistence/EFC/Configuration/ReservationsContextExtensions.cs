// path: src/Reservations/Infrastructure/Persistence/EFC/Configuration/ReservationsContextExtensions.cs
using Microsoft.EntityFrameworkCore;
using FoodStock.Reservations.Infrastructure.Persistence.EFC.Configuration;

namespace FoodStock.Reservations.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ReservationsContextExtensions
{
    public static void ApplyReservationsConfiguration(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TableEntityTypeConfiguration());
        builder.ApplyConfiguration(new ReservationEntityTypeConfiguration());
    }
}