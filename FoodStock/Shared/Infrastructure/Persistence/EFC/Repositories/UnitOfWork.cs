using FoodStock.Shared.Domain.Repositories;
using FoodStock.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FoodStock.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    // inheritedDoc
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}