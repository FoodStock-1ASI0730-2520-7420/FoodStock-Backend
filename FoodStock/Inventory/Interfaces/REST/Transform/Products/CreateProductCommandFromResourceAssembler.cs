using FoodStock.Inventory.Domain.Model.Commands.Products;
using FoodStock.Inventory.Interfaces.REST.Resources.Products;

namespace FoodStock.Inventory.Interfaces.REST.Transform.Products;
public static class CreateProductCommandFromResourceAssembler
{
    public static CreateProductCommand ToCommand(CreateProductResource r) =>
        new(r.Name, r.UnitPrice, r.Quantity, r.ExpirationDate, r.Category);
}