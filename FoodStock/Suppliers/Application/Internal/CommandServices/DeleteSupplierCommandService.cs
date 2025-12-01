using FoodStock.Suppliers.Domain.Repositories;

namespace FoodStock.Suppliers.Application.Internal.CommandServices;

public class DeleteSupplierCommandService
{
    private readonly ISupplierRepository _repository;

    public DeleteSupplierCommandService(ISupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}