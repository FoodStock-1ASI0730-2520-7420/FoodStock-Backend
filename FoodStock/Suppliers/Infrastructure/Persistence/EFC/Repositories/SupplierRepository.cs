using FoodStock.Shared.Infrastructure.Persistence.EFC.Configuration;
using FoodStock.Suppliers.Domain.Model.Aggregate;
using FoodStock.Suppliers.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Suppliers.Infrastructure.Persistence.EFC.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly AppDbContext _context;

    public SupplierRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        return await _context.Suppliers.AsNoTracking().ToListAsync();
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Supplier> AddAsync(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }

    public async Task<Supplier> UpdateAsync(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
        if (existing is null) return false;

        _context.Suppliers.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}