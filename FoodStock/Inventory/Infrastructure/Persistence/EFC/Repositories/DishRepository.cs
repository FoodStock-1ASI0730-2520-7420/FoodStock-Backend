using FoodStock.Inventory.Domain.Model.Aggregates;
using FoodStock.Inventory.Domain.Repositories;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Inventory.Infrastructure.Persistence.EFC.Repositories;
public class DishRepository : IDishRepository
{
    private readonly AppDbContext _context;
    public DishRepository(AppDbContext context) => _context = context;

    public Task<Dish?> FindByIdAsync(long id) =>
        _context.Set<Dish>().Include(d => d.Ingredients).FirstOrDefaultAsync(x => x.Id == id);

    public Task<List<Dish>> ListAsync() =>
        _context.Set<Dish>().Include(d => d.Ingredients).ToListAsync();

    public async Task AddAsync(Dish entity) => await _context.Set<Dish>().AddAsync(entity);
    public void Update(Dish entity) => _context.Set<Dish>().Update(entity);
    public void Remove(Dish entity) => _context.Set<Dish>().Remove(entity);
}