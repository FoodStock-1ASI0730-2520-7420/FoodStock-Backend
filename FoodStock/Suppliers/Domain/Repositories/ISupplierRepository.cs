using FoodStock.Suppliers.Domain.Model.Aggregate;

namespace FoodStock.Suppliers.Domain.Repositories;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAllAsync();
    Task<Supplier?> GetByIdAsync(int id);
    Task<Supplier> AddAsync(Supplier supplier);
    Task<Supplier> UpdateAsync(Supplier supplier);
    Task<bool> DeleteAsync(int id);
}