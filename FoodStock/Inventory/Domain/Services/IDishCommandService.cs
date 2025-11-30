using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Domain.Model.Commands.Dishes;

namespace FoodStock.Inventory.Domain.Services;
public interface IDishCommandService
{
    Task<Dish> Handle(CreateDishCommand command);
    Task<Dish> Handle(EditDishCommand command);
}