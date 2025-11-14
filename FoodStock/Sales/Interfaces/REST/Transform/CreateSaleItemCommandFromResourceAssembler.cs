using FoodStock.Sales.Domain.Model.Commands;
using FoodStock.Sales.Interfaces.REST.Resources;

namespace FoodStock.Sales.Interfaces.REST.Transform;

public static class CreateSaleItemCommandFromResourceAssembler
{
    public static CreateSaleItemCommand ToCommandFromResource(CreateSaleItemResource resource)
    {
        return new CreateSaleItemCommand(resource.DishId, resource.Quantity);
    }
}