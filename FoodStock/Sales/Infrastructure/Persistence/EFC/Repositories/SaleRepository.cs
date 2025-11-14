using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Repositories;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Configuration;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Sales.Infrastructure.Persistence.EFC.Repositories;

public class SaleRepository(AppDbContext context) : BaseRepository<Sale>(context), ISaleRepository
{
    public new async Task<IEnumerable<Sale>> ListAsync()
    {
        return await Context.Set<Sale>()
            .ToListAsync();
    }

    public new async Task<Sale?> FindByIdAsync(int id)
    {
        return await Context.Set<Sale>()
            .FirstOrDefaultAsync(sale => sale.Id == id);
    }
}