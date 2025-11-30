using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Domain.Repositories;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Inventory.Infrastructure.Persistence.EFC.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) => _context = context;

    public Task<Product?> FindByIdAsync(long id) => _context.Set<Product>().FirstOrDefaultAsync(x => x.Id == id);
    public Task<List<Product>> ListAsync() => _context.Set<Product>().Where(p => !p.Deleted).ToListAsync();
    public async Task AddAsync(Product entity) => await _context.Set<Product>().AddAsync(entity);
    public void Update(Product entity) => _context.Set<Product>().Update(entity);
    public void Remove(Product entity) => _context.Set<Product>().Remove(entity);
}