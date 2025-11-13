using FoodStock.Inventory.Domain.Model.Commands.Products;
using FoodStock.Inventory.Interfaces.REST.Resources.Products;

namespace FoodStock.Inventory.Interfaces.REST.Transform.Products;
public static class ConsumeProductCommandFromResourceAssembler
{
    public static ConsumeProductCommand ToCommand(long id, ConsumeProductResource r) =>
        new(id, r.Amount);
}