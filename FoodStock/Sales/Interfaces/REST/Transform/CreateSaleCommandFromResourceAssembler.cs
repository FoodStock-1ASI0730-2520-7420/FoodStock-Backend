using FoodStock.Sales.Domain.Model.Commands;
using FoodStock.Sales.Interfaces.REST.Resources;

namespace FoodStock.Sales.Interfaces.REST.Transform;

public static class CreateSaleCommandFromResourceAssembler
{
    public static CreateSaleCommand ToCommandFromResource(CreateSaleResource resource)
    {
        return new CreateSaleCommand(
            resource.SaleType,
            resource.PaymentMethod,
            resource.Waiter,
            resource.SaleItems.Select(CreateSaleItemCommandFromResourceAssembler.ToCommandFromResource).ToList());
    }
}