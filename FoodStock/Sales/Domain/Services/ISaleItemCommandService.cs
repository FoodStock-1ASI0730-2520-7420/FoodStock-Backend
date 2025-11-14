using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Model.Commands;

namespace FoodStock.Sales.Domain.Services;

public interface ISaleItemCommandService
{
    public Task<SaleItem?> Handle(CreateSaleItemCommand command);
}