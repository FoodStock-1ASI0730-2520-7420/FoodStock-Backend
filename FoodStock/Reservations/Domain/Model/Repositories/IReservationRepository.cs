// path: src/Reservations/Domain/Repositories/IReservationRepository.cs
using FoodStock.Reservations.Domain.Model.Aggregates;

namespace FoodStock.Reservations.Domain.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> ListAsync(CancellationToken ct = default);
    Task<Reservation?> FindByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(Reservation reservation, CancellationToken ct = default);
    Task UpdateAsync(Reservation reservation, CancellationToken ct = default);
    Task RemoveAsync(Reservation reservation, CancellationToken ct = default);
    Task<IEnumerable<Reservation>> ListByDateAndTableAsync(string date, int tableNumber, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}