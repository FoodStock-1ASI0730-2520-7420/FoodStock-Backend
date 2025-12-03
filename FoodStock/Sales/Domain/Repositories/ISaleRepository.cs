using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Shared.Domain.Repositories;

namespace FoodStock.Sales.Domain.Repositories;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<Sale?> FindByIdWithSaleItemsAsync(int id);
}