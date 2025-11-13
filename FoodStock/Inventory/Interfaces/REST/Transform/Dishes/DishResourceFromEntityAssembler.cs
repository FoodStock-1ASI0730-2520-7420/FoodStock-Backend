using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Interfaces.REST.Resources.Dishes;

namespace FoodStock.Inventory.Interfaces.REST.Transform.Dishes;
public static class DishResourceFromEntityAssembler
{
    public static DishResource ToResource(Dish e) =>
        new(e.Id, e.Name, e.Ingredients.Select(i => new DishIngredientResource(i.ProductId, i.Quantity)));
}