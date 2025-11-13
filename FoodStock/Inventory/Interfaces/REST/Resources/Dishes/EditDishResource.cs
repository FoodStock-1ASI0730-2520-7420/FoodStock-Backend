namespace FoodStock.Inventory.Interfaces.REST.Resources.Dishes;

public record EditDishResource(string Name, decimal PriceUnit, IEnumerable<DishIngredientResource> Ingredients);
