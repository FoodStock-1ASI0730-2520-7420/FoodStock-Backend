using FoodStock.Inventory.Domain.Model.Commands.Dishes;
using FoodStock.Inventory.Interfaces.REST.Resources.Dishes;

namespace FoodStock.Inventory.Interfaces.REST.Transform.Dishes;
public static class EditDishCommandFromResourceAssembler
{
    public static EditDishCommand ToCommand(long id, EditDishResource r) =>
        new(id, r.Name, r.Ingredients.Select(i => (i.ProductId, i.Quantity)));
}