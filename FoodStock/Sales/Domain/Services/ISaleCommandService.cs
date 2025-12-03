using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Domain.Model.Commands;

namespace FoodStock.Sales.Domain.Services;

public interface ISaleCommandService
{
    public Task<Sale?> Handle(CreateSaleCommand command);
    public Task<Sale?> Handle(UpdateSaleCommand command);
    public Task Handle(DeleteSaleCommand command);
}