using FoodStock.Sales.Domain.Model.Aggregate;
using FoodStock.Sales.Interfaces.REST.Resources;

namespace FoodStock.Sales.Interfaces.REST.Transform;

public class SaleResourceFromEntityAssembler
{
    public static SaleResource ToResourceFromEntity(Sale entity)
    {
        return new SaleResource(
            entity.Id,
            entity.SaleType,
            entity.PaymentMethod,
            entity.Total,
            entity.Waiter,
            entity.SaleItems.Select(SaleItemResourceFromEntityAssembler.ToResourceFromEntity).ToList());
    }
}