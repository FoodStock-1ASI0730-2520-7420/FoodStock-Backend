using FoodStock.Inventory.Domain.Model.Aggregates;

namespace FoodStock.Inventory.Domain.Repositories;
public interface IProductRepository
{
    Task<Product?> FindByIdAsync(long id);
    Task<List<Product>> ListAsync();
    Task AddAsync(Product entity);
    void Update(Product entity);
    void Remove(Product entity);
}