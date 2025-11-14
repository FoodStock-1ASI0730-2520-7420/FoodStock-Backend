using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Repositories;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Configuration;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Sales.Infrastructure.Persistence.EFC.Repositories;

public class SaleItemRepository(AppDbContext context) : BaseRepository<SaleItem>(context), ISaleItemRepository
{
    public async Task<IEnumerable<SaleItem>> FindBySaleIdAsync(int saleId)
    {
        return await Context.Set<SaleItem>()
            .Where(item => item.SaleId == saleId)
            .ToListAsync();
    }

    public new async Task<SaleItem?> FindByIdAsync(int id)
    {
        return await Context.Set<SaleItem>()
            .FirstOrDefaultAsync(item => item.Id == id);
    }
}