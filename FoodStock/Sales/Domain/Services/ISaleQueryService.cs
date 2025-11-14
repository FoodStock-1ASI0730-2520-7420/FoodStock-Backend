using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Model.Queries;

namespace FoodStock.Sales.Domain.Services;

public interface ISaleQueryService
{
    Task<Sale?> Handle(GetSaleByIdQuery query);
    Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query);
}