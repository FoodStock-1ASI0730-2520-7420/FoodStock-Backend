// path: src/Reservations/Application/Internal/QueryServices/ReservationQueryService.cs
using FoodStock.Reservations.Domain.Model.Aggregates;
using FoodStock.Reservations.Domain.Repositories;

namespace FoodStock.Reservations.Application.Internal.QueryServices;

public class ReservationQueryService(IReservationRepository repo)
{
    public Task<IEnumerable<Reservation>> ListAsync(CancellationToken ct = default) => repo.ListAsync(ct);
    public Task<Reservation?> FindByIdAsync(int id, CancellationToken ct = default) => repo.FindByIdAsync(id, ct);
}