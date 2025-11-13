namespace FoodStock.Inventory.Interfaces.REST.Resources.Dishes;

public record CreateDishResource(string Name, decimal PriceUnit, IEnumerable<DishIngredientResource> Ingredients);