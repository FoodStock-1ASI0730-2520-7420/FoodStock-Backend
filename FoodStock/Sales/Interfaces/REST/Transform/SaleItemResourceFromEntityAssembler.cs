using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Interfaces.REST.Resources;

namespace FoodStock.Sales.Interfaces.REST.Transform;

public class SaleItemResourceFromEntityAssembler
{
    public static SaleItemResource ToResourceFromEntity(SaleItem entity)
    {
        return new SaleItemResource(
            entity.Id,
            entity.DishId,
            entity.Name,
            entity.PriceUnit,
            entity.Quantity);
    }
}