namespace FoodStock.Inventory.Interfaces.REST.Resources.Dishes;
public record EditDishResource(string Name, IEnumerable<DishIngredientResource> Ingredients);