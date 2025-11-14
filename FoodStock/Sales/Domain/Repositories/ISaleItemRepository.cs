using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Shared.Domain.Repositories;

namespace FoodStock.Sales.Domain.Repositories;

public interface ISaleItemRepository : IBaseRepository<SaleItem>
{
    Task<IEnumerable<SaleItem>> FindBySaleIdAsync(int saleId);
}