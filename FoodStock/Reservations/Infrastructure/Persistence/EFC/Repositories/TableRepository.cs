// path: src/Reservations/Infrastructure/Persistence/EFC/Repositories/TableRepository.cs
using FoodStock.Reservations.Domain.Model.Aggregates;
using FoodStock.Reservations.Domain.Repositories;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Reservations.Infrastructure.Persistence.EFC.Repositories;

public class TableRepository(AppDbContext context) : ITableRepository
{
    public async Task<IEnumerable<Table>> ListAsync(CancellationToken ct = default)
        => await context.Set<Table>().AsNoTracking().OrderBy(t => t.Number).ToListAsync(ct);

    public async Task<Table?> FindByIdAsync(int id, CancellationToken ct = default)
        => await context.Set<Table>().FindAsync([id], ct);

    public async Task<Table?> FindByNumberAsync(int number, CancellationToken ct = default)
        => await context.Set<Table>().FirstOrDefaultAsync(t => t.Number == number, ct);

    public async Task AddAsync(Table table, CancellationToken ct = default)
        => await context.Set<Table>().AddAsync(table, ct);

    public async Task RemoveAsync(Table table, CancellationToken ct = default)
    {
        context.Set<Table>().Remove(table);
        await Task.CompletedTask;
    }

    public async Task<bool> NumberExistsAsync(int number, CancellationToken ct = default)
        => await context.Set<Table>().AnyAsync(t => t.Number == number, ct);

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}