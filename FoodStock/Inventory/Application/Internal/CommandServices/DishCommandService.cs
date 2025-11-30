using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Domain.Model.Commands.Dishes;
using FoodStock.Inventory.Domain.Repositories;
using FoodStock.Inventory.Domain.Services;
using FoodStock.Shared.Domain.Repositories;

namespace FoodStock.Inventory.Application.Internal.CommandServices;

public class DishCommandService : IDishCommandService
{
    private readonly IDishRepository _repo;
    private readonly IUnitOfWork _uow;

    public DishCommandService(IDishRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Dish> Handle(CreateDishCommand command)
    {
        var dish = new Dish(command.Name, command.PriceUnit);
        await _repo.AddAsync(dish);
        await _uow.CompleteAsync();

        var ingredients = command.Ingredients
            .Select(i => new DishIngredient(i.ProductId, i.Quantity, dish.Id))
            .ToList();

        dish.AddIngredients(ingredients);
        _repo.Update(dish);
        await _uow.CompleteAsync();

        return dish;
    }

    public async Task<Dish> Handle(EditDishCommand command)
    {
        var dish = await _repo.FindByIdAsync(command.Id)
                   ?? throw new KeyNotFoundException("Dish not found");

        var ingredients = command.Ingredients
            .Select(i => new DishIngredient(i.ProductId, i.Quantity, dish.Id))
            .ToList();

        dish.Edit(command.Name, command.PriceUnit, ingredients);
        
        _repo.Update(dish);
        await _uow.CompleteAsync();

        return dish;
    }
}