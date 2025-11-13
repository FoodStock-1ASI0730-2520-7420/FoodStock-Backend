using FoodStock.Inventory.Domain.Model.Commands.Dishes;
using FoodStock.Inventory.Interfaces.REST.Resources.Dishes;

namespace FoodStock.Inventory.Interfaces.REST.Transform.Dishes;
public static class CreateDishCommandFromResourceAssembler
{
    public static CreateDishCommand ToCommand(CreateDishResource r) =>
        new(r.Name, r.PriceUnit, r.Ingredients.Select(i => (i.ProductId, i.Quantity)));
}