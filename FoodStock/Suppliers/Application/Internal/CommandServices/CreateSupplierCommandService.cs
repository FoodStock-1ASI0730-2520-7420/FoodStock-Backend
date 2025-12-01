using FoodStock.Suppliers.Domain.Model.Aggregate;
using FoodStock.Suppliers.Domain.Model.Commands;
using FoodStock.Suppliers.Domain.Repositories;

namespace FoodStock.Suppliers.Application.Internal.CommandServices;

public class CreateSupplierCommandService
{
    private readonly ISupplierRepository _repository;

    public CreateSupplierCommandService(ISupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<Supplier> Handle(CreateSupplierCommand command)
    {
        var supplier = new Supplier(command);
        return await _repository.AddAsync(supplier);
    }
}