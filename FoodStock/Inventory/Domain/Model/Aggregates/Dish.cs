namespace FoodStock.Inventory.Domain.Model.Aggregates;

public class Dish
{
    protected Dish() { }

    public Dish(string name)
    {
        Name = name;
        Ingredients = new List<DishIngredient>();
    }

    public long Id { get; private set; }
    public string Name { get; private set; }

    public List<DishIngredient> Ingredients { get; private set; }

    public void AddIngredients(List<DishIngredient> ingredients)
    {
        Ingredients.AddRange(ingredients);
    }

    public void Edit(string name, List<DishIngredient> ingredients)
    {
        Name = name;
        Ingredients = ingredients;
    }
}