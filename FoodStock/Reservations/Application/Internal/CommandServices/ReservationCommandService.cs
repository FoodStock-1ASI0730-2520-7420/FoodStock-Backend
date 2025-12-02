// path: src/Reservations/Application/Internal/CommandServices/ReservationCommandService.cs
using FoodStock.Reservations.Domain.Model.Aggregates;
using FoodStock.Reservations.Domain.Repositories;
using FoodStock.Reservations.Domain.Services;

namespace FoodStock.Reservations.Application.Internal.CommandServices;

public class ReservationCommandService(
    IReservationRepository reservations,
    ITableRepository tables,
    IAvailabilityService availability)
{
    public async Task<Reservation> CreateAsync(
        int tableNumber,
        int quantityPeople,
        string reservationDate,
        string reservationTime,
        int durationMinutes,
        string customerName,
        string customerPhone,
        CancellationToken ct = default)
    {
        // mesa y capacidad
        var table = await tables.FindByNumberAsync(tableNumber, ct) ?? throw new InvalidOperationException("La mesa no existe");
        if (quantityPeople > table.Capacity) throw new InvalidOperationException("Capacidad de mesa insuficiente");

        // disponibilidad
        var sameDay = await reservations.ListByDateAndTableAsync(reservationDate, tableNumber, ct);
        if (!availability.IsTableAvailable(sameDay, reservationTime, durationMinutes))
            throw new InvalidOperationException("Horario no disponible");

        var entity = new Reservation(tableNumber, quantityPeople, reservationDate, reservationTime,
            durationMinutes, customerName, customerPhone);

        await reservations.AddAsync(entity, ct);
        await reservations.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<Reservation> UpdateAsync(
        int id,
        int tableNumber,
        int quantityPeople,
        string reservationDate,
        string reservationTime,
        int durationMinutes,
        string? status,
        string? customerName,
        string? customerPhone,
        CancellationToken ct = default)
    {
        var current = await reservations.FindByIdAsync(id, ct) ?? throw new KeyNotFoundException("Reserva no encontrada");

        var table = await tables.FindByNumberAsync(tableNumber, ct) ?? throw new InvalidOperationException("La mesa no existe");
        if (quantityPeople > table.Capacity) throw new InvalidOperationException("Capacidad de mesa insuficiente");

        // disponibilidad (excluyéndose a sí misma)
        var sameDay = (await reservations.ListByDateAndTableAsync(reservationDate, tableNumber, ct))
            .Where(r => r.Id != id);
        if (!availability.IsTableAvailable(sameDay, reservationTime, durationMinutes))
            throw new InvalidOperationException("Horario no disponible");

        current.Update(tableNumber, quantityPeople, reservationDate, reservationTime, durationMinutes, status, customerName, customerPhone);
        await reservations.UpdateAsync(current, ct);
        await reservations.SaveChangesAsync(ct);
        return current;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var current = await reservations.FindByIdAsync(id, ct) ?? throw new KeyNotFoundException("Reserva no encontrada");
        await reservations.RemoveAsync(current, ct);
        await reservations.SaveChangesAsync(ct);
    }
}