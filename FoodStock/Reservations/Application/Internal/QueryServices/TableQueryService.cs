// path: src/Reservations/Application/Internal/QueryServices/TableQueryService.cs
using FoodStock.Reservations.Domain.Model.Aggregates;
using FoodStock.Reservations.Domain.Repositories;

namespace FoodStock.Reservations.Application.Internal.QueryServices;

public class TableQueryService(ITableRepository repo)
{
    public Task<IEnumerable<Table>> ListAsync(CancellationToken ct = default) => repo.ListAsync(ct);
    public Task<Table?> FindByIdAsync(int id, CancellationToken ct = default) => repo.FindByIdAsync(id, ct);
}