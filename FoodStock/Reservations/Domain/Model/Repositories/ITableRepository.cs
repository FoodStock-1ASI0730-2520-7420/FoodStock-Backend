// path: src/Reservations/Domain/Repositories/ITableRepository.cs
using FoodStock.Reservations.Domain.Model.Aggregates;

namespace FoodStock.Reservations.Domain.Repositories;

public interface ITableRepository
{
    Task<IEnumerable<Table>> ListAsync(CancellationToken ct = default);
    Task<Table?> FindByIdAsync(int id, CancellationToken ct = default);
    Task<Table?> FindByNumberAsync(int number, CancellationToken ct = default);
    Task AddAsync(Table table, CancellationToken ct = default);
    Task RemoveAsync(Table table, CancellationToken ct = default);
    Task<bool> NumberExistsAsync(int number, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}