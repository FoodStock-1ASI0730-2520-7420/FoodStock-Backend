using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Model.Queries;
using FoodStock.Sales.Domain.Repositories;
using FoodStock.Sales.Domain.Services;

namespace FoodStock.Sales.Application.Internal.QueryServices;

public class SaleQueryService(ISaleRepository saleRepository) : ISaleQueryService
{
    public async Task<Sale?> Handle(GetSaleByIdQuery query)
    {
        return await saleRepository.FindByIdAsync(query.SaleId);
    }

    public async Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query)
    {
        return await saleRepository.ListAsync();
    }
}