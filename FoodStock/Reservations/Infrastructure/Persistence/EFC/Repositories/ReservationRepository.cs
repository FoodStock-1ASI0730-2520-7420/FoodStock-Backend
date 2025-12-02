// path: src/Reservations/Infrastructure/Persistence/EFC/Repositories/ReservationRepository.cs
using FoodStock.Reservations.Domain.Model.Aggregates;
using FoodStock.Reservations.Domain.Repositories;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Reservations.Infrastructure.Persistence.EFC.Repositories;

public class ReservationRepository(AppDbContext context) : IReservationRepository
{
    public async Task<IEnumerable<Reservation>> ListAsync(CancellationToken ct = default)
        => await context.Set<Reservation>().AsNoTracking()
            .OrderBy(r => r.ReservationDate)
            .ThenBy(r => r.ReservationTime)
            .ToListAsync(ct);

    public async Task<Reservation?> FindByIdAsync(int id, CancellationToken ct = default)
        => await context.Set<Reservation>().FindAsync([id], ct);

    public async Task AddAsync(Reservation reservation, CancellationToken ct = default)
        => await context.Set<Reservation>().AddAsync(reservation, ct);

    public async Task UpdateAsync(Reservation reservation, CancellationToken ct = default)
    {
        context.Set<Reservation>().Update(reservation);
        await Task.CompletedTask;
    }

    public async Task RemoveAsync(Reservation reservation, CancellationToken ct = default)
    {
        context.Set<Reservation>().Remove(reservation);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Reservation>> ListByDateAndTableAsync(string date, int tableNumber, CancellationToken ct = default)
        => await context.Set<Reservation>()
            .Where(r => r.ReservationDate == date && r.TableNumber == tableNumber && r.Status != "completed")
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}