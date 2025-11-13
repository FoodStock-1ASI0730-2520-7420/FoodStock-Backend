using FoodStock.Inventory.Domain.Model.Aggregates;

namespace FoodStock.Inventory.Domain.Repositories;
public interface IDishRepository
{
    Task<Dish?> FindByIdAsync(long id);
    Task<List<Dish>> ListAsync();
    Task AddAsync(Dish entity);
    void Update(Dish entity);
    void Remove(Dish entity);
}