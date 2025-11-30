using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Domain.Model.Queries.Dishes;
using FoodStock.Inventory.Domain.Repositories;
using FoodStock.Inventory.Domain.Services;

namespace FoodStock.Inventory.Application.Internal.QueryServices;
public class DishQueryService : IDishQueryService
{
    private readonly IDishRepository _repo;
    public DishQueryService(IDishRepository repo) => _repo = repo;

    public Task<Dish?> Handle(GetDishByIdQuery query) => _repo.FindByIdAsync(query.Id);
    public Task<List<Dish>> Handle(GetAllDishesQuery query) => _repo.ListAsync();
}