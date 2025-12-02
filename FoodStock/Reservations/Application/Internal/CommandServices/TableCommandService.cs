// path: src/Reservations/Application/Internal/CommandServices/TableCommandService.cs
using FoodStock.Reservations.Domain.Model.Aggregates;
using FoodStock.Reservations.Domain.Repositories;

namespace FoodStock.Reservations.Application.Internal.CommandServices;

public class TableCommandService(ITableRepository repo)
{
    public async Task<Table> CreateAsync(int number, int capacity, CancellationToken ct = default)
    {
        if (await repo.NumberExistsAsync(number, ct))
            throw new InvalidOperationException($"La mesa {number} ya existe.");
        var table = new Table(number, capacity);
        await repo.AddAsync(table, ct);
        await repo.SaveChangesAsync(ct);
        return table;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var table = await repo.FindByIdAsync(id, ct) ?? throw new KeyNotFoundException("Mesa no encontrada");
        await repo.RemoveAsync(table, ct);
        await repo.SaveChangesAsync(ct);
    }
}