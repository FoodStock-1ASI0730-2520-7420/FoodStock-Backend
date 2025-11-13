namespace FoodStock.Inventory.Interfaces.REST.Resources.Dishes;
public record CreateDishResource(string Name, IEnumerable<DishIngredientResource> Ingredients);