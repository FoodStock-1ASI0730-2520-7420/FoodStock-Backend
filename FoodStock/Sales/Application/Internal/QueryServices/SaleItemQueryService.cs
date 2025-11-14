using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Model.Queries;
using FoodStock.Sales.Domain.Repositories;
using FoodStock.Sales.Domain.Services;

namespace FoodStock.Sales.Application.Internal.QueryServices;

public class SaleItemQueryService(ISaleItemRepository saleItemRepository) : ISaleItemQueryService
{
    public async Task<SaleItem?> Handle(GetSaleItemByIdQuery query)
    {
        return await saleItemRepository.FindByIdAsync(query.SaleItemId);
    }

    public async Task<IEnumerable<SaleItem>> Handle(GetAllSaleItemsBySaleIdQuery query)
    {
        return await saleItemRepository.FindBySaleIdAsync(query.SaleId);
    }
}