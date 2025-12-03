using FoodStock.Sales.Domain.Model.Commands;
using FoodStock.Sales.Interfaces.REST.Resources;

namespace FoodStock.Sales.Interfaces.REST.Transform;

public static class UpdateSaleCommandFromResourceAssembler
{
    public static UpdateSaleCommand ToCommandFromResource(UpdateSaleResource resource)
    {
        return new UpdateSaleCommand(
            resource.Id,
            resource.SaleType,
            resource.PaymentMethod,
            resource.Waiter,
            resource.SaleItems.Select(item => new UpdateSaleCommand.SaleItemData(item.DishId, item.Name, item.PrinceUnit, item.Quantity)).ToList());
    }
}