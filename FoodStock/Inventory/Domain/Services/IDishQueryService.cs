using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Domain.Model.Queries.Dishes;

namespace FoodStock.Inventory.Domain.Services;
public interface IDishQueryService
{
    Task<Dish?> Handle(GetDishByIdQuery query);
    Task<List<Dish>> Handle(GetAllDishesQuery query);
}