using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Interfaces.REST.Resources.Products;

namespace FoodStock.Inventory.Interfaces.REST.Transform.Products;
public static class ProductResourceFromEntityAssembler
{
    public static ProductResource ToResource(Product e) =>
        new(e.Id, e.Name, e.UnitPrice, e.Quantity, e.TotalPrice, e.ExpirationDate, e.Category, e.Deleted);
}