using FoodStock.Sales.Domain.Model.Commands;
using FoodStock.Sales.Interfaces.REST.Resources;

namespace FoodStock.Sales.Interfaces.REST.Transform;

public static class DeleteSaleCommandFromResourceAssembler
{
    public static DeleteSaleCommand ToCommandFromResource(DeleteSaleResource resource)
    {
        return new DeleteSaleCommand(resource.Id);
    }
}