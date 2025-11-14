using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Model.Queries;

namespace FoodStock.Sales.Domain.Services;

public interface ISaleItemQueryService
{
    Task<SaleItem?> Handle(GetSaleItemByIdQuery query);
    Task<IEnumerable<SaleItem>> Handle(GetAllSaleItemsBySaleIdQuery query);
}